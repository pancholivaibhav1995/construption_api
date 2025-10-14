using Construction.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        IEnumerable<T> GetAll();
        Task<int> CommitAsync();
        Task<T> GetAsyncById(Guid id);
        Task<IDbContextTransaction> BeginTransactionAsync(DbContext dbContext);
        Task RollbackTransactionAsync(IDbContextTransaction transaction);
        Task<bool> ExistsAsync(Guid id, string Name);

        Task<IEnumerable<MaterialType>> GetAllByOrgIdAsync(Guid organisationId);
        Task<MaterialType> UpdateAsync(MaterialType entity);
    }
}
