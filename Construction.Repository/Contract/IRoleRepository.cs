using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> CreateRoleAsync(Role org);
        Task<bool> ExistsByNameAsync(string roleName, Guid organisationId);
        Task CommitAsync();
        Task<List<Role>> GetAllAsync(Guid organisationId);
    }
}
