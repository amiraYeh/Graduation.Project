﻿using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services.Tokens
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		
		public async Task<string> CreateTokenAsync(AppUserChild user, UserManager<AppUserChild> userManager)
		{
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Name,user.Name),
				new Claim(ClaimTypes.Gender,user.Gender)
			};
			var userRole = await userManager.GetRolesAsync(user);

            foreach (var role in userRole)
            {
				authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

			var token = new JwtSecurityToken(

				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
				
				);
			
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
