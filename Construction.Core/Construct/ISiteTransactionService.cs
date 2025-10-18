using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface ISiteTransactionService
    {
        Task<List<SiteTransactionResponseModel>> GetAllAsync(Guid organisationId);
        Task<SiteTransactionResponseModel> AddAsync(SiteTransactionRequestModel request);
        Task<SiteTransactionResponseModel> UpdateAsync(SiteTransactionRequestModel request);
    }
}