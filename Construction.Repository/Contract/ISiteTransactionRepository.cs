using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface ISiteTransactionRepository : IGenericRepository<SiteTransaction>
    {
        Task<List<SiteTransaction>> GetAllByOrganisationAsync(Guid organisationId);
    }
}