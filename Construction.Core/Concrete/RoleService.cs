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

namespace Construction.Core.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
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

            // Mapping to DTO
            return roles.Select(role => new RoleResponseModel
            {
                Roleid = role.Roleid,
                Rolename = role.Rolename,
                Organisationid = role.Organisationid
            }).ToList();
        }

        public async Task<List<UserManagerResponseModel>> GetAllUsersByOrganisationAsync(Guid organisationId)
        {
            return await _userRepository.GetAllUsersByOrganisationAsync(organisationId);
        }
    }
}
