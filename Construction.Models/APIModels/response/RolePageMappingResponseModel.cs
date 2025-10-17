using System;

namespace Construction.Models.APIModels.response
{
    public class RolePageMappingResponseModel
    {
        public Guid RolePageMappingId { get; set; }
        public Guid RoleId { get; set; }
        public Guid PageId { get; set; }
        public Guid? OrganisationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}