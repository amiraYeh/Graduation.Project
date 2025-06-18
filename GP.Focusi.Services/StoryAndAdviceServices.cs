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
        private readonly IStoryAndAdviceRepository<Videos> _videoRepository;

        public StoryAndAdviceServices(UserManager<AppUserChild> userManager, 
									IStoryAndAdviceRepository<Advice> adviceRepository, 
									IStoryAndAdviceRepository<Story> storyRepository, 
									IStoryAndAdviceRepository<Videos> videoRepository)
		{
			_userManager = userManager;
			_adviceRepository = adviceRepository;
			_storyRepository = storyRepository;
            _videoRepository = videoRepository;
        }
		public async Task<List<string>> AllAdvices(string email)
		{
			string childClass = await getChildClass(email);

			if (childClass is null)
				return null;

			return await _adviceRepository.GetAllAsync(childClass);
		}

		public async Task<List<string>> AllStories(string email)
		{
			string childClass = await getChildClass(email);

			if (childClass is null)
				return null;

			return await _storyRepository.GetAllAsync(childClass);
		}

        public async Task<List<string>> AllVideosByClassAsync(string email)
        {
			string childClass = await getChildClass(email);

            if (childClass is null)
                return null;

			return await _videoRepository.GetAllAsync(childClass);
        }

        public async Task<string> getChildClass(string email)
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
