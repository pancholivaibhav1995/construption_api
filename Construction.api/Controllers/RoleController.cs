using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddRole([FromBody] RoleRequestModel request)
        {
            try
            {
                var result = await _roleService.AddRoleAsync(request);

                if (!result.Success)
                    return BadRequest(new { message = result.ErrorMessage });

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unexpected error", details = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles(Guid organisationId)
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync(organisationId);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to fetch roles.", error = ex.Message });
            }
        }
    }
}
