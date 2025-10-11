using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.mapper
{
    public class OrganisationMapper : Profile
    {
        public OrganisationMapper()
        {
            CreateMap<OrganisationResponseModel, Organisation>();
            CreateMap<Organisation, OrganisationResponseModel>();
        }
    }
}
