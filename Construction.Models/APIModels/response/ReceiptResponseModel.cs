using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class ReceiptResponseModel
    {
        public Guid ReceiptId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public Guid MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? SiteId { get; set; }
        public string? PaymentStatus { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? Notes { get; set; }
        public Guid? OrganisationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
