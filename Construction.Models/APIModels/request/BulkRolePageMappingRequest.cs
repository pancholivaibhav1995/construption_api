using System;
using System.Collections.Generic;

namespace Construction.Models.APIModels.request
{
    public class RoleMappingItem
    {
        public Guid roleid { get; set; }
        public List<Guid> pageIds { get; set; }
    }

    public class BulkRolePageMappingRequest
    {
        public Guid organisationId { get; set; }
        public List<RoleMappingItem> mappings { get; set; }
    }
}