using System;

namespace Construction.Models.APIModels.response
{
    public class SiteTransactionResponseModel
    {
        public Guid SiteTransactionId { get; set; }
        public Guid? SourceId { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string Notes { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseDescription { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string TransactionType { get; set; }
    }
}