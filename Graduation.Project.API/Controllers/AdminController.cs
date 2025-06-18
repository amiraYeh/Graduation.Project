using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : BaseAppController
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginDto loginDto)
        {
            var childEmail = User.FindFirstValue(ClaimTypes.Email);

            if( loginDto.Email != "focusisystem5@gmail.com")
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Unauthorized !!"));

            var user = await _userService.LoginAsync(loginDto);

            if (user is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Login Faild!!"));

            return Ok(user);
        }
    }
}
