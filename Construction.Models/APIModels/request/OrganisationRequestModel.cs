using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class OrganisationRequestModel
    {
        public Guid OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
