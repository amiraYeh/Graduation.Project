using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{
	[Authorize]
	public class TestsController : BaseAppController
	{
		private readonly IParentTestService _parentTestService;
        private readonly IChildTestService _childTestService;
       
        public TestsController(IParentTestService parentTestService, IChildTestService childTestService)
		{
			_parentTestService = parentTestService;
            _childTestService = childTestService;
        }


		[Authorize(Roles = "TestsAccess")]
		[HttpGet]
		public IActionResult TestAccess()
		{
			return Ok("You have Access to Test");
		}

		[HttpPut("ParentsTest")]
		public async Task<IActionResult> ParentsTest([FromBody]List<int> testAnswer)
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);
			
			if (testAnswer.Count < 0 || childEmail is null) 
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			
			var res = await _parentTestService.GetDistractionRatioAsync(childEmail, testAnswer);

			if (res is < 1) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok("Your Answer Saved Successfully");

		}

		[HttpPut("gameTest")]
		public async Task<IActionResult> Game(GameTestDto gameTest)
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);

			var res = await _childTestService.GameTest(childEmail, gameTest);

			if (res is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok($"You Done Game Test Successfully, {res}");

		}

		[HttpPut("videoTest")]
		public async Task<IActionResult> Video(VideoTestDto videoTest)
		{
			if (videoTest is null) 
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            
			var childEmail = User.FindFirstValue(ClaimTypes.Email);
			
			if (childEmail is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var res = await _childTestService.VideoTest(childEmail, videoTest);

			if (res is null)
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok($"You Done Video Test Successfully, {res}");
		}
        
		

    }
}
