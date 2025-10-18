using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class SiteTransactionMapper : Profile
    {
        public SiteTransactionMapper()
        {
            CreateMap<SiteTransactionRequestModel, SiteTransaction>()
                .ForMember(dest => dest.SiteTransactionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());

            CreateMap<SiteTransaction, SiteTransactionResponseModel>();
        }
    }
}