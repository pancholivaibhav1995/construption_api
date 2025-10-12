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
    public class SupplierMapper : Profile
    {
        public SupplierMapper()
        {
            CreateMap<SupplierRequestModel, Supplier>()
             .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            // Map Site to SiteResponseModel
            CreateMap<Supplier, SupplierResponseModel>();
        }
    }
}
