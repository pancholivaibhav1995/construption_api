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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly constructiondbContext _dbContext;
        public GenericRepository(constructiondbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbContext.Set<T>();
            return query;
        }

        public async Task<T> GetAsyncById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            return await dbContext.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) return;

            try
            {
                await transaction.RollbackAsync();
            }
            catch
            {
                // Log rollback failure (optional)
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }
    }
}
