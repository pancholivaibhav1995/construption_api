using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class SiteTransactionService : ISiteTransactionService
    {
        private readonly ISiteTransactionRepository _repo;
        private readonly IMapper _mapper;

        public SiteTransactionService(ISiteTransactionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<SiteTransactionResponseModel>> GetAllAsync(Guid organisationId)
        {
            var list = await _repo.GetAllByOrganisationAsync(organisationId);
            return _mapper.Map<List<SiteTransactionResponseModel>>(list);
        }

        public async Task<SiteTransactionResponseModel> AddAsync(SiteTransactionRequestModel request)
        {
            if (request == null) throw new ArgumentException("Invalid request");

            var entity = _mapper.Map<SiteTransaction>(request);
            entity.SiteTransactionId = Guid.NewGuid();
            entity.CreateDate = DateTime.UtcNow;
            // Trim TransactionType to avoid trailing spaces (DB char column or client input)
            entity.TransactionType = request.TransactionType?.Trim();

            await _repo.AddAsync(entity);
            await _repo.CommitAsync();

            return _mapper.Map<SiteTransactionResponseModel>(entity);
        }

        public async Task<SiteTransactionResponseModel> UpdateAsync(SiteTransactionRequestModel request)
        {
            if (request == null || request.SiteTransactionId == Guid.Empty) throw new ArgumentException("Invalid request");

            var existing = await _repo.GetAsyncById(request.SiteTransactionId);
            if (existing == null) throw new KeyNotFoundException("Site transaction not found");

            existing.SourceId = request.SourceId;
            existing.ExpenseCategoryId = (Guid)request.ExpenseCategoryId;
            existing.TransactionDate = request.TransactionDate;
            existing.Notes = request.Notes;
            existing.Amount = request.Amount;
            existing.ExpenseDescription = request.ExpenseDescription;
            // Trim TransactionType to remove trailing/leading whitespace
            existing.TransactionType = request.TransactionType?.Trim();
            existing.UpdateDate = DateTime.UtcNow;

            await _repo.CommitAsync();

            return _mapper.Map<SiteTransactionResponseModel>(existing);
        }
    }
}