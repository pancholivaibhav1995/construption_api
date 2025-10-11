using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IOrganisationRepository : IGenericRepository<Organisation>
    {
        Task<Organisation> CreateOrganisationAsync(Organisation org);
        Task<User> CreateUserAsync(User user);
        Task<bool> ExistByNameAsync(string name);
    }
}
