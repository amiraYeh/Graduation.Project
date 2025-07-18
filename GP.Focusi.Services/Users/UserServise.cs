﻿using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Azure.Core;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using System.ComponentModel;


namespace GP.Focusi.Services.Users
{
	public class UserServise : IUserService
	{
		private readonly UserManager<AppUserChild> _userManager;
		private readonly SignInManager<AppUserChild> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IConfiguration _configuration;
		private readonly IEmailSenderService _emailService;
        private readonly IRoleServices _roleServices;

        public UserServise(UserManager<AppUserChild> userManager, 
			SignInManager<AppUserChild> signInManager,
			ITokenService tokenService, IConfiguration configuration,
			IEmailSenderService emailService, IRoleServices roleServices)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_configuration = configuration;
			_emailService = emailService;
            _roleServices = roleServices;
        }

		public async Task<UserDto> LoginAsync(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user is null) return null;
		
			var resultPass = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
			if (!resultPass.Succeeded ) 
				return null;

			var resultConfirm = await _userManager.IsEmailConfirmedAsync(user);
			if (!resultConfirm)
				return null;
			if(loginDto.Email == "focusisystem5@gmail.com")
			{
				await CheckAndAddUserRoleAsync(new List<string> { "Admin" }, user);
				await _userManager.AddToRoleAsync(user, "Admin");
                Console.WriteLine("Adminn");
			}

			else if (!await _userManager.IsInRoleAsync(user, "User"))
			{
				var roles = new List<string> { "User", "TestsAccess" };

				var roleR = await CheckAndAddUserRoleAsync(roles, user);

				if (roleR < 1)
					return null;
			}
            var userClass = user.ChildClass;
			if(userClass is not null)
			{
				await _userManager.RemoveFromRoleAsync(user, "TestsAccess");
				await CheckAndAddUserRoleAsync(new List<string> { "ClassAccess" }, user);
			}

            return new UserDto()
			{
				Name = user.Name,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};

		}

		public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
		{
			if (await ChechEmailExistAsync(registerDto.Email)) return null;
			var user = new AppUserChild()
			{
				Email = registerDto.Email,
				Name = registerDto.Name,
				Age = registerDto.Age,
				Gender = registerDto.Gender,
				UserName = registerDto.Email.Split('@')[0],
				DateOfCreation = registerDto.DateOfCreation,
				

			};
			var result = await _userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded) return null;

			//if(! await _roleServices.IsARole("User") || ! await _roleServices.IsARole("TestsAccess"))
			//{
			//	var roleRes = await _roleServices.createRolesAsync("User");
			//             var roleRes2 = await _roleServices.createRolesAsync("TestsAccess");
			//	if (roleRes < 1 || roleRes2 < 1)
			//	{
			//		await _userManager.DeleteAsync(user);
			//		return null;
			//	}
			//}
			//await _userManager.AddToRoleAsync(user, "User");
			//         await _userManager.AddToRoleAsync(user, "TestsAccess");
			//var isUser =
			
				var roles = new List<string> { "User", "TestsAccess" };

				var roleR = await CheckAndAddUserRoleAsync(roles, user);

				if (roleR < 1)
					return null;
			
            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var encodedConfirmTolen = Encoding.UTF8.GetBytes(confirmToken);
			var validConfirmToken = WebEncoders.Base64UrlEncode(encodedConfirmTolen);

			string url = $"{_configuration["BaseURL"]}/api/Account/confirmEmail?userId={user.Id}&token={validConfirmToken}";

			var sended =_emailService.SendAnEmail(user.Email, "Confirmation Focusi Account Email","To Confirme Your Email click here\n"+ url);
			if (sended is null) return null;
			return new UserDto()
			{
				Name = user.Name,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};

		
		}
			
		public async Task<string> ConfirmAnEmailAsync(string userId, string token)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null) return null;

			var decodedToken = WebEncoders.Base64UrlDecode(token);
			var normalToken = Encoding.UTF8.GetString(decodedToken);

			var result = await _userManager.ConfirmEmailAsync(user, normalToken);

			if (!result.Succeeded) return null;

			await _userManager.ConfirmEmailAsync(user, normalToken);

			return result.ToString();
		}
		public async Task<bool> ChechEmailExistAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null;
		}

		public async Task<UserDto> LogOutAsync(string email)
		{
			if (email is null) return null;

			var user = await _userManager.FindByEmailAsync(email);

			if (user is null) return null;

			var result = new UserDto() { Email = user.Email, Name = user.Name, Token = "0" };

			await _signInManager.SignOutAsync();

			return result;

		}

		public async Task<CurrentUserDto> GetCurrentUserAsync(string email)
		{
			if (email is null) return null;

			var user = await _userManager.FindByEmailAsync(email);

			if (user is null) return null;

			return new CurrentUserDto()
			{

				Name = user.Name,
				Age = user.Age,
				//Gender = user.Gender,
				DateOfCreation = user.DateOfCreation ,
				PictureUrl = user.PictureUrl, 
				Email = user.Email,
				TotalScore = user.ChildScore,
				ChildClass = user.ChildClass
			};

		}

		public async Task<string> ForgetPasswordAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user is null) return null;

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			var encodedTolen = Encoding.UTF8.GetBytes(token);
			var validToken = WebEncoders.Base64UrlEncode(encodedTolen);
			return validToken;
			//string url = $"{_configuration["BaseURL"]}/api/Account/resetPassword?email={email}&token={validToken}";

			//var sended = _emailService.SendAnEmail(email, "Reset Password Email", "To Reset Your Password Click here\n"+ url);
			//if (sended is null) return null;
			
			
		}

		public async Task<string> resetPasswordAsync(string token, ResetPasswordDto resetPasswordDto)
		{
			var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

			if (user is null) return null;

			var decodedToken = WebEncoders.Base64UrlDecode(token);
			var normalToken = Encoding.UTF8.GetString(decodedToken);

			var result = await _userManager.ResetPasswordAsync(user, normalToken, resetPasswordDto.NewPassword);

			if (!result.Succeeded) return null;

			return result.ToString();


		}

		public async Task<int> ProfilePicture(string pictureUrl, string email)
		{
			var child = await _userManager.FindByEmailAsync(email);
			if (child is null) return 0;

			child.PictureUrl = pictureUrl;
			var res = await _userManager.UpdateAsync(child);

			if (!res.Succeeded) return 0;
			
			return 1;

		}

        public async Task<string> UpdateProfileAsync(string? email, string name, int age)
        {
			if (email is null)
				return null;
			var user = await _userManager.FindByEmailAsync(email);
			user.Name = name;
			user.Age = age;

			var res = await _userManager.UpdateAsync(user);

			if (!res.Succeeded) return null;

			return res.ToString();

        }

        public async Task<string> addUserRoleAsync(string roleName, string email)
        {
			if (email is null)
				return null;

			var child = await _userManager.FindByEmailAsync(email);
			var res = await _userManager.AddToRoleAsync(child, roleName);
			if (!res.Succeeded)
				return null; 
			return res.ToString();
        }

        public async Task<bool> IsUserHaveThisRole(string roleName, string email)
        {
            if (email is null)
                return false;

            var child = await _userManager.FindByEmailAsync(email);
			return await _userManager.IsInRoleAsync(child, roleName);
 
        }

		private async Task<int> CheckAndAddUserRoleAsync(List<string> roles, AppUserChild user)
		{
			foreach (var role in roles)
			{
				
				if (!await _roleServices.IsARole(role))
				{
					var roleRes = await _roleServices.createRolesAsync(role);

					if (roleRes < 1)
					{
						await _userManager.DeleteAsync(user);
						return -1;
					}
				}
				if (await _userManager.IsInRoleAsync(user, role))
					continue;

				await _userManager.AddToRoleAsync(user, role);

			}
			return 1;
		}
    }
}
