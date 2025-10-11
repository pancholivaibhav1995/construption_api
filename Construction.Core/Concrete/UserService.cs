using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;

namespace Construction.Core.Concrete
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IRoleRepository _roleRepository;
        protected readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper
            ) {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        //public async Task<UserReposneModel> CreateUserAsync(UserRequestModel userRequest)
        //{
        //    //get the role for user 
        //    var roleRepository = _roleRepository.GetAll().Where(x => x.Rolename == "User").FirstOrDefault();
        //    if (roleRepository != null)
        //    {
        //        var user = _mapper.Map<User>(userRequest);
        //        var userRoles = _mapper.Map<Userrole>(roleRepository);
        //        user.Userroles.Add(userRoles);
        //        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        //        await _userRepository.AddAsync(user);
        //        var result = await _userRepository.CommitAsync();
        //        return result > 0 ? new UserReposneModel { success = true } : new UserReposneModel { success = false };
        //    }
        //    else
        //    {
        //        throw new KeyNotFoundException("Role not found");
        //    }
        //}

        //public Task<List<UserManagerResponseModel>> FetchUserManager(int roleIds)
        //{
        //    var result = _userRepository.Getuserwithuserrole()
        //        .Where(x => x.Userroles.Any(y => y.Roleid == roleIds || y.Roleid == 1) && x.Isactive == true)
        //        .Select(x => new UserManagerResponseModel
        //        {
        //            Firstname = x.Firstname,
        //            Lastname = x.Lastname,
        //            Userid = x.Userid,
        //            Roleid = roleIds
        //        })
        //        .ToList();

        //    return Task.FromResult(result);
        //}
    }
}
