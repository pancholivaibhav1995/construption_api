using Construction.Core.Construct;
using Construction.Models.APIModels.request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Construction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(UserLoginRequestModel loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest);
            return Ok(result);
        }
    }
}
