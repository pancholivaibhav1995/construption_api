using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET api/supplier/getAll/5
        [HttpGet("getAllSupplier")]
        public async Task<IActionResult> GetAll()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });
            var items = await _supplierService.GetAllAsync(organisationId);
            return Ok(items);
        }

        // POST api/supplier/add
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] SupplierRequestModel dto)
        {
            if (dto == null) return BadRequest("Invalid payload");
            if (string.IsNullOrWhiteSpace(dto.SupplierName))
                return BadRequest("SupplierName is required");

            try
            {
                var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
                if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                    return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });
                dto.OrganisationId = organisationId;
                var created = await _supplierService.AddAsync(dto);
                return CreatedAtAction(nameof(GetAll), new { organisationId = created.OrganisationId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // log exception (if logger is available)
                return StatusCode(500, "An error occurred while creating supplier");
            }
        }


        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] SupplierRequestModel dto)
        {
            if (dto == null || dto.SupplierId == Guid.Empty)
                return BadRequest("Invalid supplier data");

            try
            {
                var updated = await _supplierService.UpdateAsync(dto);
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
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
