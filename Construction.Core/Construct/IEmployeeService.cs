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
    public interface IEmployeeService
    {
        Task<ServiceResult<User>> AddEmployeeAsync(EmployeeRequestModel request);
        Task<ServiceResult<User>> EditEmployeeAsync(Guid id,EmployeeRequestModel request);
        Task<List<UserWithRoleResponseModel>> GetAllEmployeesAsync(Guid OrganisationId);
        Task<UserReposneModel> UpdateUserIsActiveAsync(Guid id, bool isActive);
    }
}
