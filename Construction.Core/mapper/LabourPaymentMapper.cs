using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class LabourPaymentMapper : Profile
    {
        public LabourPaymentMapper()
        {
            CreateMap<LabourPaymentRequestModel, LabourPayment>()
                .ForMember(dest => dest.LabourPaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<LabourPayment, LabourPaymentResponseModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}