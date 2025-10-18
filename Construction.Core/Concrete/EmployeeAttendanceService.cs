using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class EmployeeAttendanceService : IEmployeeAttendanceService
    {
        private readonly IEmployeeAttendanceRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeAttendanceService(IEmployeeAttendanceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<EmployeeAttendanceResponseModel>> GetAllAsync(Guid organisationId)
        {
            var list = await _repo.GetAllByOrganisationAsync(organisationId);
            return _mapper.Map<List<EmployeeAttendanceResponseModel>>(list);
        }

        public async Task<EmployeeAttendanceResponseModel> AddAsync(EmployeeAttendanceRequestModel request)
        {
            if (request == null) throw new ArgumentException("Invalid request");

            var entity = _mapper.Map<EmployeeAttendance>(request);
            entity.AttendanceId = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity);
            await _repo.CommitAsync();

            return _mapper.Map<EmployeeAttendanceResponseModel>(entity);
        }

        public async Task<EmployeeAttendanceResponseModel> UpdateAsync(EmployeeAttendanceRequestModel request)
        {
            if (request == null || request.AttendanceId == Guid.Empty) throw new ArgumentException("Invalid request");

            var existing = await _repo.GetAsyncById(request.AttendanceId);
            if (existing == null) throw new KeyNotFoundException("Attendance record not found");

            existing.EmployeeName = request.EmployeeName;
            existing.AttendanceStatus = request.AttendanceStatus;
            existing.OvertimeHrs = request.OvertimeHrs;
            existing.SiteId = request.SiteId;
            existing.UpdateAt = DateTime.UtcNow;
            existing.UserId = request.UserId;


            await _repo.CommitAsync();

            return _mapper.Map<EmployeeAttendanceResponseModel>(existing);
        }
    }
}