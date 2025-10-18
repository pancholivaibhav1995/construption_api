using AutoMapper;
using Azure.Core;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class DashboardService : IDashboardService
{
    protected readonly IDashboardRepository _dashboardRepository;
    protected readonly ISiteRepository _siteRepository;
    protected readonly IUserRepository _userRepository;
    protected readonly IMapper _mapper;
    public DashboardService(IDashboardRepository dashboardRepository, ISiteRepository siteRepository, IUserRepository userRepository, IMapper mapper)
    {
        _dashboardRepository = dashboardRepository;
        _siteRepository = siteRepository;
        _userRepository = userRepository;
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

    public async Task<DashboardCountsResponseModel> GetDashboardCountsAsync(Guid organisationId)
    {
        // total active sites - sites with Isactive == true
        var allSites = _siteRepository.GetAll();
        var totalActiveSites = allSites.Count(s => s.Organisationid == organisationId && s.Isactive == true);

        // total completed sites - assume 'completed' indicated by Isactive == false (if different, update logic accordingly)
        var totalCompletedSites = allSites.Count(s => s.Organisationid == organisationId && s.Isactive == false);

        // total employees (team members) - users in organisation
        var allUsers = await _userRepository.GetAllUsersByOrganisationAsync(organisationId);
        var totalEmployees = allUsers.Count();

        return new DashboardCountsResponseModel
        {
            TotalActiveSites = totalActiveSites,
            TotalCompletedSites = totalCompletedSites,
            TotalEmployees = totalEmployees
        };
    }
}