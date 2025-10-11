using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class UserLoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
