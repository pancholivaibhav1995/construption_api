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
    public class MaterialMapper : Profile
    {
        public MaterialMapper()
        {
            CreateMap<MaterialRequestModel, MaterialType>();
            CreateMap<MaterialType, MaterialResponseModel>();
        }
    }
}
