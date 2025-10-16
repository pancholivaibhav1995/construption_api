using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.mapper
{
    public  class ReceiptMapper : Profile
    {
        public ReceiptMapper()
        {
            CreateMap<ReceiptRequestModel, Receipt>()
                .ForMember(dest => dest.ReceiptId, opt => opt.Ignore())   // set in service for Add
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()); // preserve existing created date

            CreateMap<Receipt, ReceiptResponseModel>();
        }
    }
}
