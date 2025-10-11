using Construction.Core.Concrete;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IRoleService roleService, IEmployeeService employeeService)
        {
            _roleService = roleService;
            _employeeService = employeeService;
        }

        // GET: api/employee/roles
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRolesAsync(Guid organisationId)
        {
            var result = await _roleService.GetAllRolesAsync(organisationId);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeRequestModel request)
        {
            var result = await _employeeService.AddEmployeeAsync(request);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditEmployeeAsync([FromBody] EmployeeRequestModel request)
        {
            var result = await _employeeService.EditEmployeeAsync(request);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEmployeesAsync(Guid OrganisationId)
        {
            var result = await _employeeService.GetAllEmployeesAsync(OrganisationId);
            return Ok(result);
        }
        [HttpPut("update-isactive/{id}")]
        public async Task<IActionResult> UpdateUserIsActiveAsync(Guid id, [FromBody] bool isActive)
        {
            var result = await _employeeService.UpdateUserIsActiveAsync(id, isActive);
            return Ok(result);
        }
    }
}
