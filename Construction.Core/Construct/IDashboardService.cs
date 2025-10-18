using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IDashboardService
    {
        Task<DashboardResponseModel> GetDashboardStatsAsync(string Email);
        Task<DashboardCountsResponseModel> GetDashboardCountsAsync(Guid organisationId, Guid? siteId = null);
    }
}
