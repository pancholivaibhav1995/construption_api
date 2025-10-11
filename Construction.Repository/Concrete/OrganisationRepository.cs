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
    public class OrganisationRepository : GenericRepository<Organisation>, IOrganisationRepository
    { 
        private readonly constructiondbContext _context;

        public OrganisationRepository(constructiondbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public async Task<Organisation> CreateOrganisationAsync(Organisation org)
        {
            _context.Organisations.Add(org);
           // await _context.SaveChangesAsync();
            return org;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ExistByNameAsync(string name)
        {
            return await _context.Organisations.AnyAsync(o => o.OrganisationName == name);
        }
    }
}
