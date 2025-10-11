using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class EmployeeRequestModel
    {
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Isactive { get; set; }
        public string Contact { get; set; }
        public decimal? Wageperday { get; set; }
        public string Address { get; set; }
        public Guid RoleId { get; set; } // If you want to assign a role

        public Guid organisationId { get; set; }
    }
}
