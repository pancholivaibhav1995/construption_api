using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
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
        private readonly IMapper _mapper;
        public SupplierService(ISupplierRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
                CreatedDate = (DateTime)s.CreatedDate,
                SupplierContactPerson = s.SupplierContactPerson,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address,
                Notes = s.Notes
            });

            return dtos;
        }

        public async Task<SupplierResponseModel> AddAsync(SupplierRequestModel dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SupplierName))
                throw new ArgumentException("SupplierName is required", nameof(dto.SupplierName));


            var normalizedName = dto.SupplierName.Trim();

            // check existence (case-insensitive)
            var exists = await _repo.ExistsAsync(dto.OrganisationId, normalizedName);
            if (exists)
                throw new ArgumentException("A supplier with the same name already exists for this organisation.");
            var entity = _mapper.Map<Supplier>(dto);
            entity.SupplierId = Guid.NewGuid();
            entity.CreatedDate = DateTime.UtcNow;   
            //var entity = new Supplier
            //{
            //    SupplierId = Guid.NewGuid(),
            //    SupplierName = dto.SupplierName.Trim(),
            //    OrganisationId = dto.OrganisationId,
            //    CreatedDate = DateTime.UtcNow
            //};

            var created = await _repo.AddAsync(entity);
            var response = _mapper.Map<SupplierResponseModel>(entity);
            return response;
        }

        // ✅ EDIT SUPPLIER LOGIC
        public async Task<SupplierResponseModel> UpdateAsync(SupplierRequestModel dto)
        {
            if (dto.SupplierId == Guid.Empty)
                throw new ArgumentException("SupplierId is required", nameof(dto.SupplierId));

            if (string.IsNullOrWhiteSpace(dto.SupplierName))
                throw new ArgumentException("SupplierName is required", nameof(dto.SupplierName));

            var existing = await _repo.GetByIdAsync(dto.SupplierId);
            if (existing == null)
                throw new KeyNotFoundException("Supplier not found");

            // check for duplicate supplier names under same organisation
            var duplicate = await _repo.ExistsAsync(dto.OrganisationId, dto.SupplierName);
            if (duplicate)
                throw new ArgumentException("A supplier with the same name already exists for this organisation.");


            existing.Email = dto.Email;
            existing.SupplierContactPerson = dto.SupplierContactPerson;
            existing.SupplierName = dto.SupplierName;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Address = dto.Address;
            existing.Notes = dto.Notes;
            existing.CreatedDate = existing.CreatedDate;
            //existing.SupplierName = dto.SupplierName.Trim();
            var saveResponse = await _repo.UpdateAsync(existing);
            var response = _mapper.Map<SupplierResponseModel>(saveResponse);
            return response;
        }
    }
}
