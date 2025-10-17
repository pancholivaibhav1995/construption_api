using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class RolePageMappingRepository : GenericRepository<RolePageMapping>, IRolePageMappingRepository
    {
        public RolePageMappingRepository(constructiondbContext context) : base(context)
        {
        }

        public async Task<List<RolePageMapping>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _dbContext.RolePageMappings
                .Where(rp => rp.OrganisationId == organisationId)
                .ToListAsync();
        }

        public async Task UpdateMappingsAsync(Guid roleId, List<Guid> pageIds)
        {
            // Load existing mappings for the role
            var existing = await _dbContext.RolePageMappings
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();

            var existingPageIds = existing.Select(e => e.PageId).ToHashSet();
            var newPageIds = (pageIds ?? new List<Guid>()).ToHashSet();

            // Determine mappings to remove (present in DB but not in new list)
            var toRemove = existing.Where(e => !newPageIds.Contains(e.PageId)).ToList();
            if (toRemove.Any())
                _dbContext.RolePageMappings.RemoveRange(toRemove);

            // Determine pageIds to add (present in new list but not in DB)
            var toAdd = newPageIds.Except(existingPageIds).ToList();

            // determine organisation id from role (if any)
            var organisationId = await _dbContext.Roles
                .Where(r => r.Roleid == roleId)
                .Select(r => r.Organisationid)
                .FirstOrDefaultAsync();

            foreach (var pid in toAdd)
            {
                _dbContext.RolePageMappings.Add(new RolePageMapping
                {
                    RolePageMappingId = Guid.NewGuid(),
                    RoleId = roleId,
                    PageId = pid,
                    OrganisationId = organisationId == Guid.Empty ? (Guid?)null : organisationId,
                    CreatedDate = DateTime.UtcNow
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}