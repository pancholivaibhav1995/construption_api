using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class SiteRepository : GenericRepository<Site>, ISiteRepository
    {
        public SiteRepository(constructiondbContext dbContext) : base(dbContext)
        {
        }

        //public List<Site> GetSiteWithManager()
        //{
        //    return _dbContext.Sites
        //        .Include(s => s.User)
        //        .ToList();
        //}
    }
}
