using System;

namespace Construction.Models.APIModels.request
{
    public class PageRequestModel
    {
        public Guid PageId { get; set; }
        public string PageName { get; set; }
        public Guid OrganisationId { get; set; }
    }
}