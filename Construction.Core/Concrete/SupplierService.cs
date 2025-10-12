using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;

        public SupplierService(ISupplierRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SupplierRequestModel>> GetAllAsync(Guid organisationId)
        {
            var items = await _repo.GetAllByOrganisationAsync(organisationId);
            // manual mapping to DTO
            var dtos = items.Select(s => new SupplierRequestModel
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                OrganisationId = s.OrganisationId,
                CreatedDate = (DateTime)s.CreatedDate
            });

            return dtos;
        }

        public async Task<SupplierRequestModel> AddAsync(SupplierRequestModel dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SupplierName))
                throw new ArgumentException("SupplierName is required", nameof(dto.SupplierName));


            var normalizedName = dto.SupplierName.Trim();

            // check existence (case-insensitive)
            var exists = await _repo.ExistsAsync(dto.OrganisationId, normalizedName);
            if (exists)
                throw new ArgumentException("A supplier with the same name already exists for this organisation.");

            var entity = new Supplier
            {
                SupplierId = Guid.NewGuid(),
                SupplierName = dto.SupplierName.Trim(),
                OrganisationId = dto.OrganisationId,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(entity);

            return new SupplierRequestModel
            {
                SupplierId = created.SupplierId,
                SupplierName = created.SupplierName,
                OrganisationId = created.OrganisationId,
                CreatedDate = (DateTime)created.CreatedDate
            };
        }
    }
}
