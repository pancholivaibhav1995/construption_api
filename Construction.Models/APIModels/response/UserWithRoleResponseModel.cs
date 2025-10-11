using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class UserWithRoleResponseModel
    {
        public Guid Userid { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        

        public string Contact { get; set; }

        public decimal? Wageperday { get; set; }

        public string Address { get; set; }

        public Guid Roleid { get; set; }
        public string Rolename { get; set; }
    }
}
