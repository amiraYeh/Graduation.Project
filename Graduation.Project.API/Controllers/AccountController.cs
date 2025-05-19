using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GP.Focusi.API.Controllers
{
	
	public class AccountController : BaseAppController
	{
		private readonly UserManager<AppUserChild> _userManager;
		private readonly IUserService _userService;

		public AccountController(UserManager<AppUserChild> userManager,IUserService userService)
		{
			_userManager = userManager;
			_userService = userService;
		}

		[HttpPost("login")]
		 public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userService.LoginAsync(loginDto);
			if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized,"Login Faild"));

			return Ok(user);
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			var user = await _userService.RegisterAsync(registerDto);
			if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invalid Registeration"));
			
			return Ok(user);
		}

		[HttpGet("logout")]
		[Authorize]
		public async Task<ActionResult> LogOut()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);

			if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var user = await _userService.LogOutAsync(userEmail);

			if (user is not null && user.Token == "0" ) return Ok(new { message = "Logged Out Successfuly" });

			return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Logged Out Falid !!"));

		}

		[HttpGet("CurrentUser")]
		[Authorize]
		public async Task<ActionResult> GetCurrentUser()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);

			if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var user = await _userService.GetCurrentUserAsync(userEmail);
			
			if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
		
			return Ok(user);
		}

		[HttpGet("confirmEmail")]
		public async Task<ActionResult> ConfirmAnEmail(string userId, string token)
		{
			if (String.IsNullOrWhiteSpace(userId) || String.IsNullOrWhiteSpace(token))
				return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

			var result = await _userService.ConfirmAnEmailAsync(userId, token);

			if (result is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok("Your Mail Confirmed Succussfly, Go To login ");

		}

		[HttpPost("forgetpassword")]
		public async Task<ActionResult> ForgetPassword(ForgetPasswordDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"Operation Falid!!"));

			var res = await _userService.ForgetPasswordAsync(model.Email);
			if (res is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Operation Falid!!"));

			return Ok(new {token= res});
		}

		

		[HttpPost("resetPassword")]
		public  async Task<ActionResult> ResetPassword([FromQuery] string token, ResetPasswordDto model)
		{

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is null) 
				return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

			var result = await _userService.resetPasswordAsync(token, model);

			if(result is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status404NotFound));

			return Ok("Password has been reseted successfuly");

		}
	}
}
