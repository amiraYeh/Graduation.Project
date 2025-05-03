using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Data.Contexts
{
	public class FocusiAppDbContext : DbContext
	{
        public FocusiAppDbContext(DbContextOptions<FocusiAppDbContext> options):base(options)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<TaskManagerItems> TaskManagerItems { get; set; }
		public DbSet<TaskManager> TaskManagers { get; set; }
		public DbSet<FeedBack> FeedBacks { get; set; }
		public DbSet<ParentTest> ParentTests { get; set; }

	}
}
