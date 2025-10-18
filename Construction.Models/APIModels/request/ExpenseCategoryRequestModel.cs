using System;

namespace Construction.Models.APIModels.request
{
    public class ExpenseCategoryRequestModel
    {
        public Guid ExpenseCategoryId { get; set; }
        public string ExpenseCategoryName { get; set; }
        public Guid OrganisationId { get; set; }
    }
}