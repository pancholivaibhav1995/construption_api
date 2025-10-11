using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class EmployeeResponseModel
    {
        public Guid Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Contact { get; set; }
        public decimal? Wageperday { get; set; }
        public string Address { get; set; }
        public int Roleid { get; set; }
        public string RoleName { get; set; }
    }
}
