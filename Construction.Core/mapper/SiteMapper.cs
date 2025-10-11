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
    public class SiteMapper :Profile
    {
       public SiteMapper ()
        {
            // Map SiteRequestModel to Site
            CreateMap<SiteRequestModel, Site>();

            // Map Site to SiteResponseModel
            CreateMap<Site, SiteResponseModel>();

            CreateMap<Site, SiteResponseModel>();
           //.ForMember(dest => dest.firstname, opt => opt.MapFrom(src => src.User != null ? src.User.Firstname : null))
           //.ForMember(dest => dest.lastname, opt => opt.MapFrom(src => src.User != null ? src.User.Lastname : null))
           //.ForMember(dest => dest.Isactive, opt => opt.MapFrom(src => src.IsActive));
        }
    }
}
