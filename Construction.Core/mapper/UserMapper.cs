
using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class UserMapper : Profile
    {
        public UserMapper() {
            CreateMap<UserRequestModel, User>();
            CreateMap<OrganisationRequestModel, User>();
            CreateMap<Role, Userrole>();
            CreateMap<EmployeeRequestModel, User>();
            CreateMap<User, EmployeeResponseModel>();
        }
    }
}
