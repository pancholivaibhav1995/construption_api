using AutoMapper;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;

namespace Construction.Core.mapper
{
    public class EmployeeAttendanceMapper : Profile
    {
        public EmployeeAttendanceMapper()
        {
            CreateMap<EmployeeAttendanceRequestModel, EmployeeAttendance>()
                .ForMember(dest => dest.AttendanceId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateAt, opt => opt.Ignore());

            CreateMap<EmployeeAttendance, EmployeeAttendanceResponseModel>();
        }
    }
}