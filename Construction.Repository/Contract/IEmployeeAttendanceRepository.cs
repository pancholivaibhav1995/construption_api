using Construction.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Repository.Contract
{
    public interface IEmployeeAttendanceRepository : IGenericRepository<EmployeeAttendance>
    {
        Task<List<EmployeeAttendance>> GetAllByOrganisationAsync(Guid organisationId);
    }
}