using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        Task<List<Page>> GetAllByOrganisationAsync(Guid organisationId);
    }
}