using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class AuthonticateResponseModel
    {
        public string Token { get; set; }
        public  Guid RoleId { get; set; }
    }
}
