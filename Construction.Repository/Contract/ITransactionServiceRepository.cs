using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface ITransactionServiceRepository
    {
        Task<IDbContextTransaction> BeginAsync();
    }
}
