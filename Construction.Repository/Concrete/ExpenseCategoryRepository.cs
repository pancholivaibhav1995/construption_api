using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(constructiondbContext context) : base(context)
        {
        }

        public async Task<List<ExpenseCategory>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _dbContext.ExpenseCategories
                .Where(ec => ec.OrganisationId == organisationId)
                .ToListAsync();
        }
    }
}