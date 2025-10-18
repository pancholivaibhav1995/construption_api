using System;

namespace Construction.Models.APIModels.response
{
    public class ExpenseCategoryResponseModel
    {
        public Guid ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public Guid? OrganisationId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}