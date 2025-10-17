using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        public PageRepository(constructiondbContext context) : base(context)
        {
        }

        public async Task<List<Page>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _dbContext.Pages
                .Where(p => p.OrganisationId == organisationId)
                .ToListAsync();
        }
    }
}