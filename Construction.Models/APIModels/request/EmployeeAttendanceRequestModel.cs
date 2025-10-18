using System;

namespace Construction.Models.APIModels.request
{
    public class EmployeeAttendanceRequestModel
    {
        public Guid AttendanceId { get; set; }
        public string EmployeeName { get; set; }
        public string AttendanceStatus { get; set; }
        public decimal? OvertimeHrs { get; set; }
        public Guid SiteId { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid UserId { get; set; }
        public DateOnly AttendanceDate { get; set; }
    }
}