using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        // POST: api/site/add
        [HttpPost("add")]
        public async Task<IActionResult> AddSiteAsync([FromBody] SiteRequestModel request)
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });
            request.OrganisationId = organisationId;
            var result = await _siteService.AddSiteAsync(request);
            return Ok(result);
        }

        // GET: api/site/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSiteByIdAsync(Guid id)
        {
            var result = await _siteService.GetSiteByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // PUT: api/site/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSiteAsync(Guid id, [FromBody] SiteRequestModel request)
        {
            var result = await _siteService.UpdateSiteAsync(id, request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // GET: api/site
        [HttpGet]
        [Route("GetAllSite")]
        public IActionResult GetAllSitesAsync()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            var result = _siteService.GetAllSitesByOrganisationId(organisationId);
            return Ok(result);
        }

    }
}
