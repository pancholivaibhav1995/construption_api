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
    public interface IRoleService
    {
        //Task<List<RoleResponseModel>> GetAllRolesAsync();
        Task<ServiceResult<Role>> AddRoleAsync(RoleRequestModel request);
        Task<List<RoleResponseModel>> GetAllRolesAsync(Guid organisationId);
    }
}
