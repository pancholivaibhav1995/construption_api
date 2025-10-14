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
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repo;
        private readonly IMapper _mapper;
        public MaterialService(IMaterialRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<IEnumerable<MaterialResponseModel>> GetAllAsync(Guid organisationId)
        {
            var list = await _repo.GetAllByOrgIdAsync(organisationId);
            return list.Select(m => new MaterialResponseModel
            {
                MaterialTypeId = m.MaterialTypeId,
                MaterialName = m.MaterialName,
                DefaultUnit = m.DefaultUnit,
                OrganisationId = m.OrganisationId,
                CreatedDate = m.CreatedDate,
                UpdatedDate = m.UpdatedDate
            });
        }

        public async Task<MaterialResponseModel> AddAsync(MaterialRequestModel dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaterialName))
                throw new ArgumentException("MaterialName is required", nameof(dto.MaterialName));

            var exists = await _repo.ExistsAsync(dto.OrganisationId, dto.MaterialName);
            if (exists)
                throw new ArgumentException("A material with the same name already exists for this organisation.");
            var entity = _mapper.Map<MaterialType>(dto);

            entity.MaterialTypeId = Guid.NewGuid();
            entity.CreatedDate = DateTime.UtcNow;
            var created = await _repo.AddAsync(entity);
            await _repo.CommitAsync();
            var result = _mapper.Map<MaterialResponseModel>(created);
            return result;
        }

        public async Task<MaterialResponseModel> UpdateAsync(MaterialRequestModel dto)
        {
            if (dto.MaterialTypeId == Guid.Empty)
                throw new ArgumentException("MaterialTypeId is required", nameof(dto.MaterialTypeId));

            if (string.IsNullOrWhiteSpace(dto.MaterialName))
                throw new ArgumentException("MaterialName is required", nameof(dto.MaterialName));

            var existing = await _repo.GetAsyncById(dto.MaterialTypeId);
            if (existing == null)
                throw new KeyNotFoundException("Material not found");

            var duplicate = await _repo.ExistsAsync(dto.OrganisationId, dto.MaterialName);
            if (duplicate)
                throw new ArgumentException("A material with the same name already exists for this organisation.");

            // Update only editable fields, preserve CreatedDate
            existing.MaterialName = dto.MaterialName.Trim();
            existing.DefaultUnit = dto.DefaultUnit;
            existing.UpdatedDate = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            var result = _mapper.Map<MaterialResponseModel>(existing);
            return result;
        }
    }
}
