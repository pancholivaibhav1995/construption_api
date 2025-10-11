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
    public class DashboardRepository : GenericRepository<Organisation>, IDashboardRepository
    {
        protected readonly constructiondbContext _constructiondbContext;
        public DashboardRepository(constructiondbContext dbContext) : base(dbContext)
        {
            _constructiondbContext = dbContext;
        }

        public async Task<Organisation> GetOrganisationByIdAsync(string emailId)
        {
            var organisationId =  _constructiondbContext.Users
            .Where(o => o.Email == emailId).Select(a => a.OrganisationId)
            .FirstOrDefaultAsync();
            return await _constructiondbContext.Organisations
                .Where(a => a.OrganisationId == organisationId.Result)
                .FirstOrDefaultAsync();
        }
    }
}
