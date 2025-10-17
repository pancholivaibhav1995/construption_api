using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface ILabourPaymentService
    {
        Task<List<LabourPaymentResponseModel>> GetAllAsync(Guid organisationId);
        Task<LabourPaymentResponseModel> AddAsync(LabourPaymentRequestModel request);
        Task<LabourPaymentResponseModel> UpdateAsync(LabourPaymentRequestModel request);

        // Get employees who have labour access in the organisation
        Task<List<UserManagerResponseModel>> GetEmployeesWithLabourAccessAsync(Guid organisationId);

        // Combined data: employees with labour role and sites list
        Task<LabourSiteDataResponseModel> GetLabourEmployeesAndSitesAsync(Guid organisationId);
    }
}