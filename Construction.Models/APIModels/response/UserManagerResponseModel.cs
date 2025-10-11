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
        public Guid Userid { get; set; }
        public Guid Roleid { get; set; }

    }
}
