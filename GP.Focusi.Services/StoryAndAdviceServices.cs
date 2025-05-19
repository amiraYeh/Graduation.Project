using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Services
{
	public class StoryAndAdviceServices : IStoryAndAdviceServices
	{
		private readonly UserManager<AppUserChild> _userManager;
		private readonly IStoryAndAdviceRepository<Advice> _adviceRepository;
		private readonly IStoryAndAdviceRepository<Story> _storyRepository;

		public StoryAndAdviceServices(UserManager<AppUserChild> userManager, IStoryAndAdviceRepository<Advice> adviceRepository, IStoryAndAdviceRepository<Story> storyRepository)
		{
			_userManager = userManager;
			_adviceRepository = adviceRepository;
			_storyRepository = storyRepository;
		}
		public async Task<IEnumerable<object>> AllAdvices(string email)
		{
			string childClass = await getChildClass(email);

			if (childClass is null)
				return null;

			return await _adviceRepository.GetAllAsync(childClass);
		}

		public async Task<IEnumerable<object>> AllStories(string email)
		{
			string childClass = await getChildClass(email);

			if (childClass is null)
				return null;

			return await _storyRepository.GetAllAsync(childClass);
		}
		private async Task<string> getChildClass(string email)
		{
			if (email is null)
				return null;

			var child = await _userManager.FindByEmailAsync(email);

			if (child is null)
				return null;

			string childClass = child.ChildClass;
			return childClass;
		}
	}
}
