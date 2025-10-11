using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Models.APIModels.response
{
    public class SiteResponseModel
    {
        public int SiteId { get; set; }
        public string Sitename { get; set; }
        public string Location { get; set; }
        public int Userid { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }    
        public bool Isactive { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
