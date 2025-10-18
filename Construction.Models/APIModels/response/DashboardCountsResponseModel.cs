using System;

namespace Construction.Models.APIModels.response
{
    public class DashboardCountsResponseModel
    {
        public int TotalActiveSites { get; set; }
        public int TotalCompletedSites { get; set; }
        public int TotalEmployees { get; set; }
    }
}