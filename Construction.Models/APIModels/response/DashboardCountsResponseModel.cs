using System;

namespace Construction.Models.APIModels.response
{
    public class DashboardCountsResponseModel
    {
        public int TotalActiveSites { get; set; }
        public int TotalCompletedSites { get; set; }
        public int TotalEmployees { get; set; }

        public decimal TotalLabourPaymentPaid { get; set; }
        public decimal TotalSupplierPaymentPending { get; set; }
        public decimal TotalSupplierPaymentDone { get; set; }
        public decimal TotalCreditTransactions { get; set; }
        public decimal TotalDebitTransactions { get; set; }

        // Derived
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
    }
}