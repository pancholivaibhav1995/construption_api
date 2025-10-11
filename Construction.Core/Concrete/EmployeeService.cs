using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    // Construction.Core/Concrete/EmployeeService.cs
    using AutoMapper;
    using Construction.Core.Construct;
    using Construction.Entity.Models;
    using Construction.Models.APIModels.request;
    using Construction.Models.APIModels.response;
    using Construction.Repository.Concrete;
    using Construction.Repository.Contract;
    using System.Data;
    using System.Threading.Tasks;

    public class EmployeeService : IEmployeeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        protected readonly IUserRoleRepository _userRoleRepository;
        protected readonly ITransactionService _transactionService;

        public EmployeeService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IUserRoleRepository userRoleRepository, ITransactionService transactionService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _transactionService = transactionService;
        }

        public async Task<ServiceResult<User>> AddEmployeeAsync(EmployeeRequestModel request)
        {
            var transaction = await _transactionService.BeginAsync();
            try
            {
                var user = _mapper.Map<User>(request);
                user.Userid = Guid.NewGuid();
                user.Contactnumber = request.Contact;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.OrganisationId = request.organisationId;

                await _userRepository.AddAsync(user);

                // 4. Link User to Role
                var userRole = new Userrole
                {
                    Userroleid = Guid.NewGuid(),
                    Roleid = request.RoleId,
                    UserId = user.Userid // <-- Make sure this is included
                };
                await _userRoleRepository.CreateUserRoleAsync(userRole);
                await _userRepository.CommitAsync();
                await transaction.CommitAsync();
                return ServiceResult<User>.Ok(user);
            }
            catch(Exception ex)
            {
                await _transactionService.RollbackAsync(transaction);
                return ServiceResult<User>.Fail("Failed to add employee. " + ex.Message);
            }
        }

        public async Task<ServiceResult<User>> EditEmployeeAsync(EmployeeRequestModel request)
        {
            var user = await _userRepository.GetAsyncById(request.UserId);
            if (user == null)
                return ServiceResult<User>.Fail("User Not Found. " );

            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.City = request.City;
            user.Email = request.Email;
            user.Isactive = request.Isactive;
            user.Contactnumber = request.Contact;
            user.Wageperday = request.Wageperday;
            user.Address = request.Address;

            // Only update password if provided
            if (!string.IsNullOrWhiteSpace(request.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Update role if needed
            if (request.RoleId != Guid.Empty)
            {
                var existingUserRole = _userRoleRepository.GetByUserIdAsync(request.UserId);
                if (existingUserRole == null)
                    return ServiceResult<User>.Fail("UserRole Not Found. ");
                if (existingUserRole != null)
                {
                    existingUserRole.Result.Roleid = request.RoleId;
                }
            }
            user.UpdatedDate = DateTime.UtcNow;

            var result = await _userRepository.CommitAsync();
            return ServiceResult<User>.Ok(user);
        }

        public async Task<List<UserWithRoleResponseModel>> GetAllEmployeesAsync(Guid OrganisationId)
        {
            return await _userRepository.GetUserWithRolesByIdAsync(OrganisationId);
        }
        public async Task<UserReposneModel> UpdateUserIsActiveAsync(Guid id, bool isActive)
        {
            var user = await _userRepository.GetAsyncById(id);
            if (user == null)
                return new UserReposneModel { success = false };

            user.Isactive = isActive;
            var result = await _userRepository.CommitAsync();
            return new UserReposneModel { success = result > 0 };
        }
    }

}
