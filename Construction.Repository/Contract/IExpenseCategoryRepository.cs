using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory>
    {
        Task<List<ExpenseCategory>> GetAllByOrganisationAsync(Guid organisationId);
    }
}