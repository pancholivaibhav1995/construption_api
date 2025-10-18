using System;

namespace Construction.Models.APIModels.request
{
    public class SiteTransactionRequestModel
    {
        public Guid SiteTransactionId { get; set; }
        public Guid? SourceId { get; set; }
        public Guid? ExpenseCategoryId { get; set; }
        public DateOnly TransactionDate { get; set; }
        public string Notes { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseDescription { get; set; }
        public Guid OrganisationId { get; set; }
        public string TransactionType { get; set; }
    }
}