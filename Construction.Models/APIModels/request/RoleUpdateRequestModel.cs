using System;

namespace Construction.Models.APIModels.request
{
    public class RoleUpdateRequestModel
    {
        public Guid Roleid { get; set; }
        public string Rolename { get; set; }
        // Organisationid will be set from JWT claims in controller
        public Guid Organisationid { get; set; }
    }
}