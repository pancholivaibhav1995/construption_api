using Construction.Core.Construct;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.Entity.Models;
using Construction.Repository.Contract;

namespace Construction.Core.Concrete
{
    public class TransactionService : ITransactionService
    {
        protected readonly ITransactionServiceRepository _repository;

        public TransactionService(ITransactionServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDbContextTransaction> BeginAsync()
        {
            return await _repository.BeginAsync();
        }

        public async Task RollbackAsync(IDbContextTransaction transaction)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
            }
        }
    }

}
