using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.request
{
    public class SiteRequestModel
    {
        public string Sitename { get; set; }
        public string Location { get; set; }
        public int Userid { get; set; }
        public bool Isactive { get; set; }
    }
}
