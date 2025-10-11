using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface ITransactionService
    {
        Task<IDbContextTransaction> BeginAsync();
        Task RollbackAsync(IDbContextTransaction transaction);
    }
}
