using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs.Auth
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Email is Required !")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Name is Required !")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Age is Required !")]
		[Range(4,12, ErrorMessage ="Age Must be between 4 to 12 year")]
		public int Age { get; set; }

		[Required(ErrorMessage = "Gender is Required !")]
		public string Gender { get; set; }

		[Required(ErrorMessage = "Password is Required !")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Comfirm Password is Required !")]
		[Compare(nameof(Password),ErrorMessage = "Confirmed Password dosn't match Passwod !")]
		public string ConfirmPassword { get; set; }

		 //date = DateOnly.FromDateTime(DateTime.Now);
		public DateTime DateOfCreation { get; } = DateTime.Now.Date;

	}
}
