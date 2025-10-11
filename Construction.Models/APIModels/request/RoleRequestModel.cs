using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class RoleRequestModel
    {
        public string Rolename { get; set; }
        public Guid Organisationid { get; set; }
    }
}
