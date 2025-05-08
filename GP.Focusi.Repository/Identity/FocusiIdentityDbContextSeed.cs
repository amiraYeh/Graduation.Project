//using GP.Focusi.Core.Entites.Identity;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GP.Focusi.Repository.Identity
//{
//	public static class FocusiIdentityDbContextSeed
//	{
     
//        public static async Task SeedUserAsync(UserManager<AppUserChild> userManager)
//		{
//			if(userManager.Users.Count() == 0)
//			{
//				var user = new AppUserChild()
//				{
//					Email = "ahmedAmine@gmail.com",
//					Name = "Ahmed Amine",
//					Age = 10,
//					Gender = "Male",
//					UserName= "ahmedAmine@gmail.com".Split('@')[0],
//				};
//				await userManager.CreateAsync(user, "Pa$$wod");
//			}			
//		}
//	}
//}
