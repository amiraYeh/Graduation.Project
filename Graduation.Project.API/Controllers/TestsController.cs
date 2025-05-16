using GP.Focusi.APIs.Errors;
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

		public TestsController(IParentTestService parentTestService)
		{
			_parentTestService = parentTestService;
		}
		[HttpPut("ParentsTest")]
		public async Task<IActionResult> ParentsTest(List<int> testAnswer)
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);

			if (testAnswer.Count < 0 || childEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			
			var res = await _parentTestService.GetDistractionRatioAsync(childEmail, testAnswer);

			if (res is < 1) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok("Your Answer Saved Successfully");

		}
	}
}
