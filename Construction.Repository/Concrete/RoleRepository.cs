using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Models.APIModels.response;

namespace Construction.Repository.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly constructiondbContext _context;
        public RoleRepository(constructiondbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role> CreateRoleAsync(Role org)
        {
            _context.Roles.Add(org);
           // await _context.SaveChangesAsync();
            return org;
        }

        public async Task<bool> ExistsByNameAsync(string roleName, Guid organisationId)
        {
            return await _context.Roles.AnyAsync(r => r.Rolename == roleName && r.Organisationid == organisationId);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetAllAsync(Guid organisationId)
        {
            return await _context.Roles.Where(a => a.Organisationid == organisationId).ToListAsync();
        }

        public async Task<List<UserManagerResponseModel>> GetUsersByRoleAndOrganisationAsync(Guid roleId, Guid organisationId)
        {
            // This method is required by the interface but should be implemented in UserRepository, not here.
            throw new NotImplementedException();
        }
        // Implement any specific methods for RoleRepository here, if needed
        // For example, you might want to add methods to find roles by name or other criteria
    }
}
