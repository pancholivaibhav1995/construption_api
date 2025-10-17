using System;

namespace Construction.Models.APIModels.response
{
    public class LabourPaymentResponseModel
    {
        public Guid LabourPaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public Guid SiteId { get; set; }
        public DateTime PaymentPeriodStartDate { get; set; }
        public DateTime PaymentPeriodEndDate { get; set; }
        public Guid? OrganisationId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}