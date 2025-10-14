using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class MaterialRequestModel
    {
        public Guid MaterialTypeId { get; set; }          // for update; empty for add
        public string MaterialName { get; set; }
        public string DefaultUnit { get; set; }
        public Guid OrganisationId { get; set; }
    }
}
