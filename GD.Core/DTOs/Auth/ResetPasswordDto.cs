using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs.Auth
{
	public class ResetPasswordDto
	{
		[Required(ErrorMessage = "Email is Required !")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Enter the new password !")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Comfirm Password is Required !")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirmed Password dosn't match Passwod !")]
		public string ConfirmPassword { get; set; }

		//[Required]
		//public string Token { get; set; }
    }
}
