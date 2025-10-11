using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class UserRequestModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Isactive { get; set; } = false; // Default value is false
    }
}
