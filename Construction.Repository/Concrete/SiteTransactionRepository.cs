using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class SiteTransactionRepository : GenericRepository<SiteTransaction>, ISiteTransactionRepository
    {
        public SiteTransactionRepository(constructiondbContext context) : base(context)
        {
        }

        public async Task<List<SiteTransaction>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _dbContext.SiteTransactions
                .Where(s => s.OrganisationId == organisationId)
                .ToListAsync();
        }
    }
}