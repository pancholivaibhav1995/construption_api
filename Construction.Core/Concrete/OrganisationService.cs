using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Concrete;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IOrganisationRepository _repository;
        protected readonly IUserRepository _userRepository;
        protected readonly IRoleRepository _roleRepository;
        protected readonly IMapper _mapper;
        protected readonly IUserRoleRepository _userRoleRepository;
        protected readonly ITransactionService _transactionService;

        public OrganisationService(IOrganisationRepository repository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IUserRoleRepository userRoleRepository,
            ITransactionService transactionService)
        {
            _repository = repository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _transactionService = transactionService;
        }

        public async Task<OrganisationResponseModel> CreateOrganisationAsync(OrganisationRequestModel request)
        {
            // Validate Organisation Name
            var orgExists = await _repository.ExistByNameAsync(request.OrganisationName);
            if (orgExists)
                return new OrganisationResponseModel
                {
                    ErrorMessage = "Organisation name already exists.",
                    Success = false
                };

            // Validate Email
            var emailExists = await _userRepository.ExistsByEmailAsync(request.Email);
            if (emailExists)
                return new OrganisationResponseModel
                {
                    ErrorMessage = "Email address already exists.",
                    Success = false
                };
            var transaction = await _transactionService.BeginAsync();
            try
            {
                // 1. Create Organisation
                var organisation = new Organisation
                {
                    OrganisationId = Guid.NewGuid(),
                    OrganisationName = request.OrganisationName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = null,
                    IsActive = true
                };
                await _repository.CreateOrganisationAsync(organisation);

                // 2. Create Admin Role
                var role = new Role
                {
                    Roleid = Guid.NewGuid(),
                    Organisationid = organisation.OrganisationId,
                    Rolename = "Admin"
                };
                await _roleRepository.CreateRoleAsync(role);

                // 3. Create User
                var user = _mapper.Map<User>(request);
                user.Userid = Guid.NewGuid();
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.OrganisationId = organisation.OrganisationId;
                user.CreatedDate = DateTime.UtcNow;
                user.Isactive = true;

                await _userRepository.AddAsync(user);

                // 4. Link User to Role
                var userRole = new Userrole
                {
                    Userroleid = Guid.NewGuid(),
                    Roleid = role.Roleid,
                    UserId  = user.Userid // <-- Make sure this is included
                };
                await _userRoleRepository.CreateUserRoleAsync(userRole);

                await _repository.CommitAsync();
                await transaction.CommitAsync();

                return new OrganisationResponseModel
                {
                    Success = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                await _transactionService.RollbackAsync(transaction);
                return new OrganisationResponseModel
                {
                    ErrorMessage = "An error occurred while creating the organisation.",
                    Success = false
                };
            }
        }

    }

}