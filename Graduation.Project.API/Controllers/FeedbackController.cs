using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.DTOs;
using GP.Focusi.Core.RepositoriesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{
	[Authorize]
	public class FeedbackController : BaseAppController
	{
		private readonly IFeedBackRepository _feedBackRepository;

		public FeedbackController(IFeedBackRepository feedBackRepository)
		{
			_feedBackRepository = feedBackRepository;
		}
		[HttpPost]
		public async Task<IActionResult> GiveFeedBack(FeedBackDto feedBack)
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);

			if (childEmail is null || feedBack is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var res = await _feedBackRepository.AddFeedBackAsync(childEmail, feedBack);

			if(res < 1) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok("Your Feedback Saved Successfully :)");
		}
	}
}
