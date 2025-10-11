using Construction.Entity.Models;
using Construction.Models.APIModels.response;
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

        public async Task<List<SiteResponseModel>> GetAllSiteByOrganisationId(Guid organisationId)
        {
            var result = (from site in _dbContext.Sites
                          join user in _dbContext.Users on site.Userid equals user.Userid into userJoin
                          from user in userJoin.DefaultIfEmpty()
                          where site.Organisationid == organisationId
                          select new SiteResponseModel
                          {
                              SiteId = site.Siteid,
                              Sitename = site.Sitename,
                              Location = site.Location,
                              Isactive = site.Isactive,
                              Userid = site.Userid ?? Guid.Empty,
                              firstname = user.Firstname,
                              lastname = user.Lastname
                          }).ToList();
            return result;
        }
    }
}
