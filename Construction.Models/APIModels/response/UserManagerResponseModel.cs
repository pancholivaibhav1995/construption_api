using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class UserManagerResponseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Userid { get; set; }
        public int Roleid { get; set; }

    }
}
