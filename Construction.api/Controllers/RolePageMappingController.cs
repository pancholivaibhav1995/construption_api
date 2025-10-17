using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePageMappingController : ControllerBase
    {
        private readonly IRolePageMappingService _service;

        public RolePageMappingController(IRolePageMappingService service)
        {
            _service = service;
        }

        [HttpGet("GetAllMapping")]
        public async Task<IActionResult> GetAll()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            var items = await _service.GetAllAsync(organisationId);
            return Ok(items);
        }

        [HttpPut("UpdateMappings/{roleId}")]
        public async Task<IActionResult> UpdateMappings(Guid roleId, [FromBody] List<Guid> pageIds)
        {
            if (roleId == Guid.Empty) return BadRequest("Invalid role id");
            await _service.UpdateMappingsAsync(roleId, pageIds ?? new List<Guid>());
            return Ok(new { success = true });
        }

        // Bulk update endpoint to match frontend payload
        [HttpPut("BulkUpdate")]
        public async Task<IActionResult> BulkUpdate([FromBody] BulkRolePageMappingRequest request)
        {
            if (request == null || request.mappings == null)
                return BadRequest("Invalid payload");

            // Validate organisationId from token matches payload
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationIdFromToken))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            if (request.organisationId != organisationIdFromToken)
                return Forbid();

            foreach (var item in request.mappings)
            {
                if (item.roleid == Guid.Empty) continue;
                await _service.UpdateMappingsAsync(item.roleid, item.pageIds ?? new List<Guid>());
            }

            return Ok(new { success = true });
        }
    }
}