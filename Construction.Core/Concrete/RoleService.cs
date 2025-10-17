using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Construction.Core.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task AddAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Role>> AddRoleAsync(RoleRequestModel request)
        {
            if (await _roleRepository.ExistsByNameAsync(request.Rolename, request.Organisationid))
                return ServiceResult<Role>.Fail("Role with the same name already exists.");

            var role = new Role
            {
                Roleid = Guid.NewGuid(),
                Rolename = request.Rolename,
                Organisationid = request.Organisationid
            };

            await _roleRepository.AddAsync(role);
            await _roleRepository.CommitAsync();

            return ServiceResult<Role>.Ok(role);
        }

        public async Task<List<RoleResponseModel>> GetAllRolesAsync(Guid organisationId)
        {
            var roles = await _roleRepository.GetAllAsync(organisationId);

            // Use AutoMapper to map to DTO
            return _mapper.Map<List<RoleResponseModel>>(roles);
        }

        public async Task<List<UserManagerResponseModel>> GetAllUsersByOrganisationAsync(Guid organisationId)
        {
            return await _userRepository.GetAllUsersByOrganisationAsync(organisationId);
        }

        public async Task<ServiceResult<Role>> UpdateRoleAsync(RoleUpdateRequestModel request)
        {
            if (request == null)
                return ServiceResult<Role>.Fail("Invalid request.");

            var existingRole = await _roleRepository.GetAsyncById(request.Roleid);
            if (existingRole == null)
                return ServiceResult<Role>.Fail("Role not found.");

            // Ensure role belongs to the organisation from token
            if (existingRole.Organisationid != request.Organisationid)
                return ServiceResult<Role>.Fail("Role does not belong to the current organisation.");

            // If Rolename changed, ensure no duplicate in same organisation
            if (!string.Equals(existingRole.Rolename, request.Rolename, StringComparison.OrdinalIgnoreCase))
            {
                var nameExists = await _roleRepository.ExistsByNameAsync(request.Rolename, request.Organisationid);
                if (nameExists)
                    return ServiceResult<Role>.Fail("Role with the same name already exists.");
            }

            existingRole.Rolename = request.Rolename;
            await _roleRepository.CommitAsync();

            return ServiceResult<Role>.Ok(existingRole);
        }
    }
}
