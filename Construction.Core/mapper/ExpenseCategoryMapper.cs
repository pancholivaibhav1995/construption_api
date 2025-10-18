using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class ExpenseCategoryMapper : Profile
    {
        public ExpenseCategoryMapper()
        {
            CreateMap<ExpenseCategoryRequestModel, ExpenseCategory>()
                .ForMember(dest => dest.ExpenseCategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore());

            CreateMap<ExpenseCategory, ExpenseCategoryResponseModel>();
        }
    }
}