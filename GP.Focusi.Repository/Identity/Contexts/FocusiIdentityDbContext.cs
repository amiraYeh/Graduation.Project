using GP.Focusi.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Identity.Contexts
{
	public class FocusiIdentityDbContext : IdentityDbContext<AppUserChild>
	{
		public FocusiIdentityDbContext(DbContextOptions<FocusiIdentityDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(builder);
			builder.Entity<IdentityRole>().HasData(
				      new IdentityRole { Id = "1", Name = "Admin",NormalizedName = "ADMIN"},
                           new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
						   new IdentityRole { Id = "3", Name = "TestsAccess", NormalizedName = "TESTSACCESS" },
                           new IdentityRole { Id = "4", Name = "ClassAccess", NormalizedName = "CLASSACCESS" }


                );

			//	var hasher = new PasswordHasher<AppUserChild>();

			//	var adminUser = new AppUserChild
			//	{
			//		UserName = "focusisystem5",
			//		NormalizedUserName = "FOCUSISYSTEM5",
			//		Email = "focusisystem5@gmail.com",
			//		NormalizedEmail = "FOCUSISYSTEM5@GMAIL.COM",
			//		PhoneNumber = "1234567890",
			//		EmailConfirmed = true,
			//		Age =30,
			//		Name = "focusii",
			//		Gender ="Male"
			//	};
			//	adminUser.PasswordHash = hasher.HashPassword(adminUser, "Foc_Admin123");

			//	builder.Entity<AppUserChild>().HasData(adminUser);

			//	builder.Entity<IdentityUserRole<string>>().HasData(
			//			new IdentityUserRole<string>
			//			{
			//				RoleId = "1",
			//				UserId = adminUser.Id
			//			}
			//		);
		}

	}
}
