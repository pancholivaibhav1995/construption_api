using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _service;

        public ReceiptController(IReceiptService service)
        {
            _service = service;
        }

        // GET api/receipt/GetAll/{organisationId?}
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });
            var items = await _service.GetAllAsync(organisationId);
            return Ok(items);
        }

        // POST api/receipt/Add
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ReceiptRequestModel dto)
        {
            if (dto == null) return BadRequest("Invalid payload");
            try
            {
                var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
                if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                    return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });
                dto.OrganisationId = organisationId;
                var created = await _service.AddAsync(dto);
                return Ok(created);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        // PUT api/receipt/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ReceiptRequestModel dto)
        {
            if (dto == null || dto.ReceiptId == Guid.Empty) return BadRequest("Invalid payload");
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
