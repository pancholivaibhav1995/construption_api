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

        //// POST: api/site/add
        //[HttpPost("add")]
        //public async Task<IActionResult> AddSiteAsync([FromBody] SiteRequestModel request)
        //{
        //    var result = await _siteService.AddSiteAsync(request);
        //    return Ok(result);
        //}

        //// GET: api/site/{id}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetSiteByIdAsync(int id)
        //{
        //    var result = await _siteService.GetSiteByIdAsync(id);
        //    if (result == null)
        //        return NotFound();
        //    return Ok(result);
        //}

        //// PUT: api/site/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateSiteAsync(int id, [FromBody] SiteRequestModel request)
        //{
        //    var result = await _siteService.UpdateSiteAsync(id, request);
        //    if (result == null)
        //        return NotFound();
        //    return Ok(result);
        //}

        //// GET: api/site
        //[HttpGet]
        //[Route("GetAllSite")]
        //public IActionResult GetAllSitesAsync()
        //{
        //    var result = _siteService.GetAllSitesAsync();
        //    return Ok(result);
        //}

    }
}
