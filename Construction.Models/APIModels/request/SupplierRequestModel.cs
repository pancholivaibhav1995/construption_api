using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class SupplierRequestModel
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
