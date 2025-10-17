using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class RolePageMappingMapper : Profile
    {
        public RolePageMappingMapper()
        {
            CreateMap<RolePageMapping, RolePageMappingResponseModel>()
                .ForMember(dest => dest.RolePageMappingId, opt => opt.MapFrom(src => src.RolePageMappingId))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
                .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(src => src.OrganisationId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));
        }
    }
}