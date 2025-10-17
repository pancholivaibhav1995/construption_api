using System;

namespace Construction.Models.APIModels.response
{
    public class PageResponseModel
    {
        public Guid PageId { get; set; }
        public string PageName { get; set; }
        public Guid? OrganisationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}