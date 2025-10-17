using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IRolePageMappingRepository : IGenericRepository<RolePageMapping>
    {
        Task<List<RolePageMapping>> GetAllByOrganisationAsync(Guid organisationId);
        Task UpdateMappingsAsync(Guid roleId, List<Guid> pageIds);
    }
}