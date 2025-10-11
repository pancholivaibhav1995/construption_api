using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class TransactionServiceRepository : ITransactionServiceRepository
    {
        protected readonly constructiondbContext constructiondbContext;
        public TransactionServiceRepository(constructiondbContext constructiondbContext)
        {
            this.constructiondbContext = constructiondbContext;
        }

        public async Task<IDbContextTransaction> BeginAsync()
        {
            return await constructiondbContext.Database.BeginTransactionAsync();
        }
    }
}
