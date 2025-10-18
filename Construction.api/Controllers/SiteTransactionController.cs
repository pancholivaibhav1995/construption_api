using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteTransactionController : ControllerBase
    {
        private readonly ISiteTransactionService _service;
        private readonly IExpenseCategoryService _expenseService;

        public SiteTransactionController(ISiteTransactionService service, IExpenseCategoryService expenseService)
        {
            _service = service;
            _expenseService = expenseService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            var items = await _service.GetAllAsync(organisationId);
            return Ok(items);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] SiteTransactionRequestModel dto)
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] SiteTransactionRequestModel dto)
        {
            if (dto == null || dto.SiteTransactionId == Guid.Empty) return BadRequest("Invalid payload");

            try
            {
                var updated = await _service.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        // Expense category endpoints
        [HttpGet("ExpenseCategories")]
        public async Task<IActionResult> GetExpenseCategories()
        {
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            var items = await _expenseService.GetAllAsync(organisationId);
            return Ok(items);
        }

        [HttpPost("AddExpenseCategory")]
        public async Task<IActionResult> AddExpenseCategory([FromBody] ExpenseCategoryRequestModel dto)
        {
            if (dto == null) return BadRequest("Invalid payload");
            var orgIdClaim = User.FindFirst("OrganisationId")?.Value;
            if (string.IsNullOrEmpty(orgIdClaim) || !Guid.TryParse(orgIdClaim, out var organisationId))
                return Unauthorized(new { message = "OrganisationId claim missing or invalid in token." });

            dto.OrganisationId = organisationId;

            try
            {
                var created = await _expenseService.AddAsync(dto);
                return Ok(created);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }

        [HttpPut("UpdateExpenseCategory")]
        public async Task<IActionResult> UpdateExpenseCategory([FromBody] ExpenseCategoryRequestModel dto)
        {
            if (dto == null || dto.ExpenseCategoryId == Guid.Empty) return BadRequest("Invalid payload");

            try
            {
                var updated = await _expenseService.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (Exception ex) { return StatusCode(500, new { message = ex.Message }); }
        }
    }
}