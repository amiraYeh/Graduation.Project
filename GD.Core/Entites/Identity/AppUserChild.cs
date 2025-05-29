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
		public DateTime DateOfCreation { set; get; }    //= DateOnly.FromDateTime(DateTime.Now);
		public int ChildScore { get; set; } = 0;
        public string? ChildClass { get; set; }
		public string? PictureUrl { get; set; } = null;

    }
}
