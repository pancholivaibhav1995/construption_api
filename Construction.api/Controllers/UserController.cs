using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;
        protected readonly IOrganisationService _organisationService;
        public UserController(IUserService userService, IOrganisationService organisationService)
        {
            _userService = userService;
            _organisationService = organisationService;
        }
        //[HttpPost]
        //[Route("createuserasync")]
        //public async Task<IActionResult> CreateUserAsync(UserRequestModel userRequest)
        //{
        //    var result = await _userService.CreateUserAsync(userRequest);
        //    return Ok(result);
        //}

        [HttpPost]
        [Route("registerorg")]
        public async Task<IActionResult> RegisterOrg(OrganisationRequestModel orgRequest)
        {
            // Call the organisation service to register the organisation
            try
            {
                var result = await _organisationService.CreateOrganisationAsync(orgRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    details = ex.Message
                });
            }
             
        }
        //[HttpGet]
        //[Route("getuserwithroleid")]
        //public async Task<IActionResult> FetchUserManager(int Roleid)
        //{
        //    var result = _userService.FetchUserManager(Roleid);
        //    return Ok(result);
        //}
    }
}
