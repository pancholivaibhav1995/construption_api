using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class LabourPaymentRepository : GenericRepository<LabourPayment>, ILabourPaymentRepository
    {
        public LabourPaymentRepository(constructiondbContext context) : base(context)
        {
        }

        public async Task<List<LabourPayment>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _dbContext.LabourPayments
                .Where(lp => lp.OrganisationId == organisationId)
                .ToListAsync();
        }

        public async Task<List<LabourPayment>> GetPaymentsBySiteAsync(Guid siteId)
        {
            return await _dbContext.LabourPayments
                .Where(lp => lp.SiteId == siteId)
                .ToListAsync();
        }
    }
}