using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class MaterialResponseModel
    {
        public Guid MaterialTypeId { get; set; }
        public string MaterialName { get; set; }
        public string DefaultUnit { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
