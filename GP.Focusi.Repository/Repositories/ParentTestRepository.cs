using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Repository.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Repositories
{
	public class ParentTestRepository : IParentTestRepository
	{
		private readonly FocusiAppDbContext _context;
		private readonly UserManager<AppUserChild> _userManager;

		public ParentTestRepository(FocusiAppDbContext context, UserManager<AppUserChild> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public async Task<int> AddParentTestAnswerAsync(string childEmail, int DistractionRatio)
		{
			var child  = await _userManager.FindByEmailAsync(childEmail);

			if (child is null) return 0;

			var chClass = child.ChildClass;

			if (chClass is not null) // It Mean that user Do the test pefore
			{
				var roleR = await _userManager.RemoveFromRoleAsync(child, "TestsAccess");				
			}

			var parent_test = mapParentTest(childEmail, DistractionRatio);
			if(parent_test is null)	return 0;

			await _context.AddAsync(parent_test);
			return _context.SaveChanges();
		}
		private ParentTest mapParentTest(string childEmail, int DistractionRatio)
		{
			var res = new ParentTest()
			{
				ChildEmail = childEmail,
				DistractionRatio = DistractionRatio
			};
			return res;
		}
	}
}
