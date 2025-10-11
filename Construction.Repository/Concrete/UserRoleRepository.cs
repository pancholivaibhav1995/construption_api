using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class UserRoleRepository : GenericRepository<Userrole>, IUserRoleRepository
    {
        private readonly constructiondbContext _context;
        public UserRoleRepository(constructiondbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Userrole> CreateUserRoleAsync(Userrole userRole)
        {
            _context.Userroles.Add(userRole);
           // await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task<Userrole> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Userroles.FirstOrDefaultAsync(ur => ur.UserId == userId);
        }
    }
}
