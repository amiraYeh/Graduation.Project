using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Identity
{
	public static class FocusiIdentityDbContextSeed
	{
      
		public static async Task SeedUserAsync(UserManager<AppUserChild> userManager)
		{
			if (await userManager.FindByEmailAsync("focusisystem5@gmail.com") == null)
			{
				var user = new AppUserChild()
				{
					Email = "focusisystem5@gmail.com",
					Name = "Focusi",
					Age = 30,
					Gender = "Male",
					UserName = "focusisystem5@gmail.com".Split('@')[0],
					EmailConfirmed = true,
					NormalizedEmail = "FOCUSISYSTEM5@GMAIL.COM",
					NormalizedUserName = "FOCUSISYSTEM5",
					
				};
				var res = await userManager.CreateAsync(user, "Pa$$0wod");


            }
		}
	}
}
