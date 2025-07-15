using GP.Focusi.API.Attributes;
using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.Entites.Identity;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace GP.Focusi.API.Controllers
{
	[Authorize]
    public class ChildClassController : BaseAppController
    {
		private readonly IStoryAndAdviceServices _storyAndAdviceServices;
		private readonly IClassServices _classServices;
		private string CChilddClass;
		public ChildClassController(IStoryAndAdviceServices storyAndAdviceServices, IClassServices classServices)
		{
			_storyAndAdviceServices = storyAndAdviceServices;
			_classServices = classServices;
		}

        

        [HttpGet("Advice")]
		//[Cached(10)]
        public async Task<IActionResult> getAdvicesAsync()
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);
			if (childEmail is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
			var res = await _storyAndAdviceServices.AllAdvices(childEmail);
			if(res is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok(res);
		}
		[HttpGet("Story")]
		//[Cached(10)]
        public async Task<IActionResult> getStoriesAsync()
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);
			if (childEmail is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
			var storiesName = await _storyAndAdviceServices.AllStories(childEmail);
			if (storiesName is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			
			var res = await storyMapAsync(storiesName);

			return Ok(res);
		}

		[HttpPut("Video")]
		public async Task<IActionResult> addVideoScore(VideoDto video)
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);

			var res = await _classServices.addVideoData(video, childEmail);

			if(res is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok("You Answer Saved Successfully and Your score is updated");
		}

		[HttpPut("Game")]
		public async Task<IActionResult> addGameScore([FromBody]int gameDuration)
		{
            var childEmail = User.FindFirstValue(ClaimTypes.Email);
			var res = await _classServices.gameDuragion(gameDuration, childEmail);

            if (res is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok("Your Score is updated");

        }
		[HttpGet("allVideos")]
		//[Cached(10,)]
		public async Task<IActionResult> getVideosByClass()
		{
            var childEmail = User.FindFirstValue(ClaimTypes.Email);
			 CChilddClass = await _storyAndAdviceServices.getChildClass(childEmail);
			if(childEmail is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

			var videosName = await _storyAndAdviceServices.AllVideosByClassAsync(childEmail);
			
			if(videosName is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

			var res = await videoMapAsync(videosName);

            return Ok(res);
        }

        private async Task<List<StoryDto>> storyMapAsync(List<string> stories)
		{
			var res = new List<StoryDto>();

			foreach (var story in stories)
			{
				StoryDto storyDto = new StoryDto
				{
					StoryName = story,
					StoryUrl = $"{Request.Scheme}://{Request.Host}/AllStories/{story}.pdf",
					CoverPageUrl = $"{Request.Scheme}://{Request.Host}/StoriesCovers/{story}.png"
				};
                res.Add(storyDto);
			}
			return res;
		}

		private async Task<List<ClassVideoDto>> videoMapAsync(List<string> videos)
		{
			var res = new List<ClassVideoDto>();

			foreach(var video in videos)
			{
				//D:\NewDownloads\VS&CB\C#\Graduation.Project.Solution\Graduation.Project.API\wwwroot\Class Videos\audio\A1animals_choices.mp3
				string folderPath = @".\wwwroot\Class Videos\audio";
				List<object> audioNames = new List<object>();
                object audioUrls;
				if (Directory.Exists(folderPath))
				{
					string[] audios = Directory.GetFiles(folderPath);
					foreach (string audio in audios)
					{
						if (audio.Contains(video))
						{
							var path = Path.GetFileName(audio);
                            int start = audio.IndexOf('_') + 1;
                            int end = audio.LastIndexOf('.');
                            int length = end - start;

                            string name = audio.Substring(start, length);

                            audioNames.Add(new Dictionary<string,string> {[name]= $"{Request.Scheme}://{Request.Host}/Class Videos/audio/{path}" });
                        }
					}
					ClassVideoDto videoDto = new ClassVideoDto
					{
						VideoName = video,
						VideoUrl = $"{Request.Scheme}://{Request.Host}/Class Videos/Videos/{video}.mp4",
						AudiosUrl = audioNames

					};
					//videoDto.Questions = _storyAndAdviceServices.getVideQuestions(videoDto.VideoName);
					res.Add(videoDto);
				}
			}
			return res;

		}
		//private List<ClassVideoDto> getVideo(List<string> videos)
		//{
		//	foreach (var video in videos)
		//	{
		//		var videoDto = new ClassVideoDto 
		//		{ 
		//			VideoName = video,
		//			AudiosUrl = new List<string> { $"{Request.Scheme}://{Request.Host}/Class Videos/audio/" }
		//		};
		//	}
		//}
		
	}
}
