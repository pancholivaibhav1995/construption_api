using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IUserRoleRepository : IGenericRepository<Userrole>
    {
       Task<Userrole> GetByUserIdAsync(Guid userId);

        Task<Userrole> CreateUserRoleAsync(Userrole userRole);
    }
}
