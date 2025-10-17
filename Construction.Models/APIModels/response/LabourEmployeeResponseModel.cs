using System;

namespace Construction.Models.APIModels.response
{
    public class LabourEmployeeResponseModel
    {
        public Guid Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Guid Roleid { get; set; }
        public decimal? Wageperday { get; set; }
    }
}