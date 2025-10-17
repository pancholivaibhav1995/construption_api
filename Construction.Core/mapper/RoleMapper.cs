using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleRequestModel, Role>()
                .ForMember(dest => dest.Roleid, opt => opt.Ignore());

            CreateMap<Role, RoleResponseModel>();

            CreateMap<RoleUpdateRequestModel, Role>()
                .ForMember(dest => dest.Roleid, opt => opt.MapFrom(src => src.Roleid));
        }
    }
}