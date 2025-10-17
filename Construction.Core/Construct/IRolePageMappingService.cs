using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IRolePageMappingService
    {
        Task<List<RolePageMappingResponseModel>> GetAllAsync(Guid organisationId);
        Task UpdateMappingsAsync(Guid roleId, List<Guid> pageIds);
    }
}