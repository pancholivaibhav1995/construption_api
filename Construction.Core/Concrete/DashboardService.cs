//using Construction.Core.Construct;
//using Construction.Entity.Models;
//using Construction.Models.APIModels.response;
//using Construction.Repository.Contract;

using AutoMapper;
using Azure.Core;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;

public class DashboardService : IDashboardService
{
    protected readonly IDashboardRepository _dashboardRepository;
    protected readonly IMapper _mapper;
    public DashboardService(IDashboardRepository   dashboardRepository, IMapper mapper)
    {
        _dashboardRepository = dashboardRepository;
        _mapper = mapper;
    }

    public async Task<DashboardResponseModel> GetDashboardStatsAsync(string emailID)
    {
        var organisationDetail = await _dashboardRepository.GetOrganisationByIdAsync(emailID);
        return new DashboardResponseModel
        {
            OrganisationId = organisationDetail.OrganisationId,
            OrganisationName = organisationDetail.OrganisationName
        };
    }
}