using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IExpenseCategoryService
    {
        Task<List<ExpenseCategoryResponseModel>> GetAllAsync(Guid organisationId);
        Task<ExpenseCategoryResponseModel> AddAsync(ExpenseCategoryRequestModel request);
        Task<ExpenseCategoryResponseModel> UpdateAsync(ExpenseCategoryRequestModel request);
    }
}