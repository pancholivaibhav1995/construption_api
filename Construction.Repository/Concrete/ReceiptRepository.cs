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
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        private readonly constructiondbContext _db;
        public ReceiptRepository(constructiondbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public async Task<IEnumerable<Receipt>> GetAllAsync(Guid? organisationId = null)
        {
            var q = _db.Receipts.AsQueryable();
            if (organisationId.HasValue)
                q = q.Where(r => r.OrganisationId == organisationId.Value);

            return await q.OrderByDescending(r => r.ReceiptDate).ToListAsync();
        }

        public async Task<Receipt?> GetByIdAsync(Guid receiptId)
        {
            return await _db.Receipts.FirstOrDefaultAsync(r => r.ReceiptId == receiptId);
        }

        public async Task<Receipt> AddAsync(Receipt entity)
        {
            _db.Receipts.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Receipt> UpdateAsync(Receipt entity)
        {
            var tracked = _db.ChangeTracker.Entries<Receipt>()
                .FirstOrDefault(e => e.Entity.ReceiptId == entity.ReceiptId);

            if (tracked == null)
            {
                // entity likely detached - attach/update
                _db.Receipts.Update(entity);
            }
            else
            {
                // if tracked, update current values
                tracked.CurrentValues.SetValues(entity);
            }

            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
