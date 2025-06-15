using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{
	[Authorize(Roles ="TestsAccess")]
	public class TestsController : BaseAppController
	{
		private readonly IParentTestService _parentTestService;
        private readonly IRoleServices _roleServices;
        private readonly IUserService _userService;

        public TestsController(IParentTestService parentTestService,IRoleServices roleServices, IUserService userService)
		{
			_parentTestService = parentTestService;
            _roleServices = roleServices;
            _userService = userService;
        }
		//private async Task<int> addRoleAsync(string roleName)
		//{
		//	return await _roleServices.createRolesAsync(roleName);
		//}
		//private async Task giveRoleToUser(string roleName,string email)
		//{
  //          int roleRes = 0;
  //          if (roleRes != 1)
  //              roleRes = await	addRoleAsync(roleName);

			

  //      }
		[HttpPut("ParentsTest")]
		public async Task<IActionResult> ParentsTest([FromBody]List<int> testAnswer)
		{
			var childEmail = User.FindFirstValue(ClaimTypes.Email);
			//int roleRes = 0;
			//if (roleRes != 1) 
			//	roleRes = await addRoleAsync();
			//var chId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			

			if (testAnswer.Count < 0 || childEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			
			var res = await _parentTestService.GetDistractionRatioAsync(childEmail, testAnswer);

			if (res is < 1) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok("Your Answer Saved Successfully");

		}
		
	}
}
