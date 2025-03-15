using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs.Auth
{
	public class CurrentUserDto
	{
		public string Name { get; set; }

		public int Age { get; set; }
		public string Gender { get; set; }
		public string Token { get; set; }
		public DateTime DateOfCreation { set; get; } 

	}
}
