using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public  interface ISiteRepository : IGenericRepository<Site>
    {
       // public List<Site> GetSiteWithManager();
    }
}
