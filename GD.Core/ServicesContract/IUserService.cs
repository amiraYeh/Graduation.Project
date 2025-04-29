using GP.Focusi.Core.DTOs.Auth;
using GP.Focusi.Core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.ServicesContract
{
	public interface IUserService
	{
		Task<UserDto> LoginAsync(LoginDto loginDto);
		Task<UserDto> RegisterAsync(RegisterDto registerDto);
		Task<string> ConfirmAnEmailAsync(string userId, string token);
		Task<bool> ChechEmailExistAsync(string email);
		Task<UserDto> LogOutAsync(string email);
		Task<CurrentUserDto> GetCurrentUserAsync(string email);

		Task ForgetPasswordAsync(string email);

		Task<string> resetPasswordAsync(string token, ResetPasswordDto resetPasswordDto);

	}
}
