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
    public class SupplierRepository : ISupplierRepository
    {
        private readonly constructiondbContext _db;

        public SupplierRepository(constructiondbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Supplier>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _db.Suppliers
                            .Where(s => s.OrganisationId == organisationId)
                            .OrderByDescending(s => s.CreatedDate)
                            .ToListAsync();
        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            _db.Suppliers.Add(supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        public async Task<bool> ExistsAsync(Guid organisationId, string supplierName)
        {
            // Normalize supplierName for case-insensitive compare
            var normalized = supplierName.Trim().ToLower();

            return await _db.Suppliers
                .AsNoTracking()
                .AnyAsync(s =>
                    s.OrganisationId == organisationId &&
                    s.SupplierName.ToLower() == normalized);
        }

        public async Task<Supplier> UpdateAsync(Supplier supplier)
        {
           // _db.Suppliers.Update(supplier);
            await _db.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier?> GetByIdAsync(Guid supplierId)
        {
            return await _db.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == supplierId);

        }
    }
}
