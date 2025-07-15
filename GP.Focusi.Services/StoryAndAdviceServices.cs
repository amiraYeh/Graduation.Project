using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.RepositoriesContract;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
		//     public List<string> getVideQuestions(string video)
		//     {
		//         if (video is null)
		//             return null;
		// var res = new List<string>();
		//         //D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\GP.Focusi.Services\StoryAndAdviceServices.cs
		//         //D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\\Data\VideoQuestions.json
		//         //D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\Graduation.Project.API\wwwroot\Class Videos\VideoQuestions.json
		//         string json = File.ReadAllText(@".\wwwroot\Class Videos\VideoQuestions.json");

		//         using JsonDocument doc = JsonDocument.Parse(json);
		//JsonElement root = doc.RootElement;

		//         foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	string videoName = element.GetProperty("videoName").GetString();
		//	string questionTag = "";

		//             if (root.TryGetProperty("Tags", out JsonElement tagsElement) && tagsElement.ValueKind == JsonValueKind.Array)
		//	{
		//		foreach (JsonElement tag in tagsElement.EnumerateArray())
		//		{
		//			if(tag.GetString() == "Questions")
		//				questionTag = tag.GetString();
		//		}
		//	}
		//             if (videoName == video)
		//		res.Add(questionTag[0].ToString());

		//		//string questions = element.GetProperty("Questions").geto

		//}
		//             //foreach (var question in questions)
		//             //{
		//             //	if(question.)
		//             //}
		//             return res;
		//     }
	}
}
