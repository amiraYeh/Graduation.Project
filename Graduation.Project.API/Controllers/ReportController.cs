using GP.Focusi.APIs.Errors;
using GP.Focusi.Core.RepositoriesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GP.Focusi.API.Controllers
{
    [Authorize]
    public class ReportController : BaseAppController
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> childReport()
        {
            var childEmail = User.FindFirstValue(ClaimTypes.Email);

            if (childEmail is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var res = await _reportRepository.getReportAsync(childEmail);

            return Ok(res);
        }
    }
}
