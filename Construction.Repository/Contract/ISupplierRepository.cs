using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllByOrganisationAsync(Guid organisationId);
        Task<Supplier> AddAsync(Supplier supplier);

        Task<bool> ExistsAsync(Guid organisationId, string supplierName);
    }
}
