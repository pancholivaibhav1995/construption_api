using System;
using System.Collections.Generic;

namespace Construction.Models.APIModels.response
{
    public class LabourSiteDataResponseModel
    {
        public List<LabourEmployeeResponseModel> Employees { get; set; }
        public List<SiteResponseModel> Sites { get; set; }
    }
}