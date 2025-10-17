using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet("GetAllPages")]
        public async Task<IActionResult> GetAll()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            var items = await _pageService.GetAllAsync(organisationId);
            return Ok(items);
        }

        [HttpPost("AddPage")]
        public async Task<IActionResult> Add([FromBody] PageRequestModel dto)
        {
            if (dto == null) return BadRequest("Invalid payload");
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });
            dto.OrganisationId = organisationId;

            try
            {
                var created = await _pageService.AddAsync(dto);
                return Ok(created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("UpdatePage")]
        public async Task<IActionResult> Update([FromBody] PageRequestModel dto)
        {
            if (dto == null || dto.PageId == Guid.Empty) return BadRequest("Invalid payload");

            try
            {
                var updated = await _pageService.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}