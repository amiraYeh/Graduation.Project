using GP.Focusi.API.Attributes;
using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.Entites;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{
	[Authorize]
    public class ChildClassController : BaseAppController
    {
		private readonly IStoryAndAdviceServices _storyAndAdviceServices;
		private readonly IClassServices _classServices;

		public ChildClassController(IStoryAndAdviceServices storyAndAdviceServices, IClassServices classServices)
		{
			_storyAndAdviceServices = storyAndAdviceServices;
			_classServices = classServices;
		}
		[HttpGet("Advice")]
		[Cached(3)]
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
        [Cached(3)]
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
	}
}
