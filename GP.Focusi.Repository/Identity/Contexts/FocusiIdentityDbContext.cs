using GP.Focusi.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
			base.OnModelCreating(builder);
		}

	}
}
