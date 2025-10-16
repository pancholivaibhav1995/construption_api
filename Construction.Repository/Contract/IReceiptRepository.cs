using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<IEnumerable<Receipt>> GetAllAsync(Guid? organisationId = null);
        Task<Receipt?> GetByIdAsync(Guid receiptId);
        Task<Receipt> AddAsync(Receipt entity);
        Task<Receipt> UpdateAsync(Receipt entity);
    }
}
