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
    public class MaterialRepository : GenericRepository<MaterialType>, IMaterialRepository
    {
        public MaterialRepository(constructiondbContext dbContext) : base(dbContext)
        {

        }


        public Task<bool> ExistsAsync(Guid id, string Name)
        {
            var normalized = Name.Trim().ToLower();
            return _dbContext.MaterialTypes
                .AsNoTracking()
                .AnyAsync(m =>
                    m.OrganisationId == id && m.MaterialName.ToLower() == normalized);
        }

        public async Task<IEnumerable<MaterialType>> GetAllByOrgIdAsync(Guid organisationId)
        {
            return await _dbContext.MaterialTypes
                .Where(m => m.OrganisationId == organisationId)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<MaterialType> UpdateAsync(MaterialType entity)
        {
            // if entity is already tracked and modified, SaveChanges is enough
            // If it's detached, attach & update
            var tracked = _dbContext.ChangeTracker.Entries<MaterialType>()
                .FirstOrDefault(e => e.Entity.MaterialTypeId == entity.MaterialTypeId);

            if (tracked == null)
            {
                _dbContext.MaterialTypes.Update(entity);
            }
            else
            {
                // tracked entity exists; copy values
                tracked.CurrentValues.SetValues(entity);
            }

            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
