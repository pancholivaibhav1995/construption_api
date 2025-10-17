using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class PageMapper : Profile
    {
        public PageMapper()
        {
            CreateMap<PageRequestModel, Page>()
                .ForMember(dest => dest.PageId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());

            CreateMap<Page, PageResponseModel>();
        }
    }
}