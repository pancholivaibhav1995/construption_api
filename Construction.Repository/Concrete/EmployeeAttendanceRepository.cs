using Construction.Entity.Models;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Repository.Concrete
{
    public class EmployeeAttendanceRepository : GenericRepository<EmployeeAttendance>, IEmployeeAttendanceRepository
    {
        public EmployeeAttendanceRepository(constructiondbContext context) : base(context)
        {
        }

        public async Task<List<EmployeeAttendance>> GetAllByOrganisationAsync(Guid organisationId)
        {
            return await _dbContext.EmployeeAttendances
                .Where(e => e.OrganisationId == organisationId)
                .ToListAsync();
        }
    }
}