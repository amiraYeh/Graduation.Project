using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites.Identity
{
	public class AppUserChild :IdentityUser
	{
		public string Name { get; set; }	

		public int Age { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfCreation { set; get; } = DateTime.Now;
	}
}
