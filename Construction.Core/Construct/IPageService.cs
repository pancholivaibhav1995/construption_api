using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IPageService
    {
        Task<List<PageResponseModel>> GetAllAsync(Guid organisationId);
        Task<PageResponseModel> AddAsync(PageRequestModel request);
        Task<PageResponseModel> UpdateAsync(PageRequestModel request);
    }
}