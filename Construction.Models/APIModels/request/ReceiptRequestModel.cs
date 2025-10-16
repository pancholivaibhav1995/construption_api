using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class ReceiptRequestModel
    {
        public Guid ReceiptId { get; set; }           // empty for Add
        public DateTime ReceiptDate { get; set; }
        public Guid MaterialId { get; set; }
        public decimal Quantity { get; set; }         // maps to DB Quntity
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }           // stored directly
        public Guid? SupplierId { get; set; }
        public Guid? SiteId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? Notes { get; set; }
        public Guid? OrganisationId { get; set; }
    }
}
