using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class RoleResponseModel
    {
        public Guid Roleid { get; set; }
        public string Rolename { get; set; }

        public Guid Organisationid { get; set; }
    }
}
