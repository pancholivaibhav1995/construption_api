using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IMaterialRepository : IGenericRepository<MaterialType>
    {
        Task<bool> ExistsAsync(Guid id, string Name);

        Task<IEnumerable<MaterialType>> GetAllByOrgIdAsync(Guid organisationId);
        Task<MaterialType> UpdateAsync(MaterialType entity);
    }
}
