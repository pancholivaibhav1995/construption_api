using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class RolePageMappingService : IRolePageMappingService
    {
        private readonly IRolePageMappingRepository _repo;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RolePageMappingService(IRolePageMappingRepository repo, IRoleRepository roleRepository, IMapper mapper)
        {
            _repo = repo;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<RolePageMappingResponseModel>> GetAllAsync(Guid organisationId)
        {
            // get all mappings for org
            var list = await _repo.GetAllByOrganisationAsync(organisationId);

            // fetch roles for organisation and find admin role ids
            var roles = await _roleRepository.GetAllAsync(organisationId);
            var adminRoleIds = roles
                .Where(r => string.Equals(r.Rolename, "Admin", StringComparison.OrdinalIgnoreCase))
                .Select(r => r.Roleid)
                .ToHashSet();

            // filter out mappings where RoleId is an admin role
            var filtered = list.Where(m => !adminRoleIds.Contains(m.RoleId)).ToList();

            return _mapper.Map<List<RolePageMappingResponseModel>>(filtered);
        }

        public async Task UpdateMappingsAsync(Guid roleId, List<Guid> pageIds)
        {
            await _repo.UpdateMappingsAsync(roleId, pageIds);
        }
    }
}