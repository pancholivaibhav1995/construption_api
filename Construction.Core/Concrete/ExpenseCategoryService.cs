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
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository _repo;
        private readonly IMapper _mapper;

        public ExpenseCategoryService(IExpenseCategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ExpenseCategoryResponseModel>> GetAllAsync(Guid organisationId)
        {
            var list = await _repo.GetAllByOrganisationAsync(organisationId);
            return _mapper.Map<List<ExpenseCategoryResponseModel>>(list);
        }

        public async Task<ExpenseCategoryResponseModel> AddAsync(ExpenseCategoryRequestModel request)
        {
            if (request == null) throw new ArgumentException("Invalid request");

            var entity = _mapper.Map<ExpenseCategory>(request);
            entity.ExpenseCategoryId = Guid.NewGuid();
            entity.CreateDate = DateTime.UtcNow;

            await _repo.AddAsync(entity);
            await _repo.CommitAsync();

            return _mapper.Map<ExpenseCategoryResponseModel>(entity);
        }

        public async Task<ExpenseCategoryResponseModel> UpdateAsync(ExpenseCategoryRequestModel request)
        {
            if (request == null || request.ExpenseCategoryId == Guid.Empty) throw new ArgumentException("Invalid request");

            var existing = await _repo.GetAsyncById(request.ExpenseCategoryId);
            if (existing == null) throw new KeyNotFoundException("Expense category not found");

            existing.ExpenseCategoryName = request.ExpenseCategoryName;

            await _repo.CommitAsync();

            return _mapper.Map<ExpenseCategoryResponseModel>(existing);
        }
    }
}