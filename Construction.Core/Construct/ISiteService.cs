using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface ISiteService
    {
        Task<SiteResponseModel> AddSiteAsync(SiteRequestModel request);
        Task<SiteResponseModel> GetSiteByIdAsync(Guid id);
        Task<SiteResponseModel> UpdateSiteAsync(Guid id, SiteRequestModel request);
        //List<SiteResponseModel> GetAllSitesAsync();
        List<SiteResponseModel> GetAllSitesByOrganisationId(Guid organisationId);
    }
}
