using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IOrganisationService
    {
        public Task<OrganisationResponseModel> CreateOrganisationAsync(OrganisationRequestModel request);
    }
}
