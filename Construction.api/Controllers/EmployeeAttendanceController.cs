using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeAttendanceController : ControllerBase
    {
        private readonly IEmployeeAttendanceService _service;

        public EmployeeAttendanceController(IEmployeeAttendanceService service)
        {
            _service = service;
        }

        [HttpGet("GetAllEmployeeAttendace")]
        public async Task<IActionResult> GetAll()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            var items = await _service.GetAllAsync(organisationId);
            return Ok(items);
        }

        [HttpPost("AddAttendace")]
        public async Task<IActionResult> Add([FromBody] EmployeeAttendanceRequestModel dto)
        {
            if (dto == null) return BadRequest("Invalid payload");
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            dto.OrganisationId = organisationId;

            try
            {
                var created = await _service.AddAsync(dto);
                return Ok(created);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPut("UpdateAttendace")]
        public async Task<IActionResult> Update([FromBody] EmployeeAttendanceRequestModel dto)
        {
            if (dto == null || dto.AttendanceId == Guid.Empty) return BadRequest("Invalid payload");

            try
            {
                var updated = await _service.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }
    }
}