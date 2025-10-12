using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class SupplierResponseModel
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? SupplierContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
    }
}
