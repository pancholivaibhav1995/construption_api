using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface ILabourPaymentRepository : IGenericRepository<LabourPayment>
    {
        Task<List<LabourPayment>> GetAllByOrganisationAsync(Guid organisationId);
        Task<List<LabourPayment>> GetPaymentsBySiteAsync(Guid siteId);
    }
}