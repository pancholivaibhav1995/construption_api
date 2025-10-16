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
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _repo;
        private readonly IMapper _mapper;

        public ReceiptService(IReceiptRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReceiptResponseModel>> GetAllAsync(Guid organisationId)
        {
            try
            {
                var list = await _repo.GetAllAsync(organisationId);
                return list.Select(r => _mapper.Map<ReceiptResponseModel>(r));
            }
            catch(Exception ex)
            {
                return Enumerable.Empty<ReceiptResponseModel>();
            }
        }

        public async Task<ReceiptResponseModel> AddAsync(ReceiptRequestModel dto)
        {
           
                if (dto == null) throw new ArgumentNullException(nameof(dto));
                if (dto.MaterialId == Guid.Empty) throw new ArgumentException("MaterialId is required");
                if (dto.Quantity <= 0) throw new ArgumentException("Quantity must be > 0");
                if (dto.Rate < 0) throw new ArgumentException("Rate must be >= 0");
                if (dto.Amount < 0) throw new ArgumentException("Amount must be >= 0");

                var entity = _mapper.Map<Receipt>(dto);
                entity.ReceiptId = Guid.NewGuid();
                entity.CreatedDate = DateTime.UtcNow;

                var created = await _repo.AddAsync(entity);
                return _mapper.Map<ReceiptResponseModel>(created);
          
           
        }

        public async Task<ReceiptResponseModel> UpdateAsync(ReceiptRequestModel dto)
        {
            if (dto == null || dto.ReceiptId == Guid.Empty) throw new ArgumentException("ReceiptId is required");
            var existing = await _repo.GetAsyncById(dto.ReceiptId);
            if (existing == null) throw new KeyNotFoundException("Receipt not found");

            // Map only editable fields onto existing. Using AutoMapper mapping onto existing preserves CreatedDate.
            _mapper.Map(dto, existing);
            existing.UpdatedDate = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            return _mapper.Map<ReceiptResponseModel>(existing);
        }
    }
}
