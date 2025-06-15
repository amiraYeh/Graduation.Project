using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{

    [Authorize(Roles = "TestsAccess")]
    public class ChildTestController : BaseAppController
    {
        private readonly IChildTestService _childTestService;

        public ChildTestController(IChildTestService childTestService)
        {
            _childTestService = childTestService;
        }

        //[HttpPut("gameTest")]
        //public async Task<IActionResult> Game()
        //{
        //    var childEmail = User.FindFirstValue(ClaimTypes.Email);

        //    var res = await _childTestService.GameTest(childEmail, 2);
            
        //    if (res is null)
        //       return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

        //    if(res == 2) // 
        //        return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"You Have been Done this test pefore"));

        //    return Ok();

        //}
    }
}
