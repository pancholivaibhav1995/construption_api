using Construction.Core.Concrete;
using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        protected readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet]
        [Route("GetDashboardData")]
        public async Task<IActionResult> DashBoardGet(string emailId)
        {
            var result = await _dashboardService.GetDashboardStatsAsync(emailId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDashboardDataForDashboard")]
        public async Task<IActionResult> DashBoardData(Guid organisationId)
        {
            var result = await _dashboardService.GetDashboardCountsAsync(organisationId);
            return Ok(result);
        }
    }
}
