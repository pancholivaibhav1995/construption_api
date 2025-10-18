using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IEmployeeAttendanceService
    {
        Task<List<EmployeeAttendanceResponseModel>> GetAllAsync(Guid organisationId);
        Task<EmployeeAttendanceResponseModel> AddAsync(EmployeeAttendanceRequestModel request);
        Task<EmployeeAttendanceResponseModel> UpdateAsync(EmployeeAttendanceRequestModel request);
    }
}