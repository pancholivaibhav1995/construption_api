using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Concrete;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SiteService(ISiteRepository siteRepository,IUserRepository userRepository, IMapper mapper)
        {
            _siteRepository = siteRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SiteResponseModel> AddSiteAsync(SiteRequestModel request)
        {
            try
            {
                var site = _mapper.Map<Site>(request);
                site.Siteid = Guid.NewGuid();
                site.Isactive = false;
                await _siteRepository.AddAsync(site);
                var result = await _siteRepository.CommitAsync();
                var response = _mapper.Map<SiteResponseModel>(site);
                if (result <= 0)
                {
                    response.Success = false;
                }
                else
                {
                    response.Success = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<SiteResponseModel> GetSiteByIdAsync(Guid id)
        {
            var site = await _siteRepository.GetAsyncById(id);
            if (site != null)
            {
                var response = _mapper.Map<SiteResponseModel>(site);
                response.Success = true;
                return response;
            }
            else
            {
                return new SiteResponseModel
                {
                    Success = false,
                    Message = "Site not found"
                };
            }
        }
        public async Task<SiteResponseModel> UpdateSiteAsync(Guid id, SiteRequestModel request)
        {
            var site = await _siteRepository.GetAsyncById(id);
            if (site == null)
                return null;

            site.Sitename = request.Sitename;
            site.Location = request.Location;
            site.Userid = request.Userid;
            site.Isactive = request.IsActive;

            var result = await _siteRepository.CommitAsync();
            var response = _mapper.Map<SiteResponseModel>(site);
            if (result <= 0)
            {
                response.Success = false;
                return response;
            }
            else
            {
                response.Success = true;
                return response;
            }
        }

        public List<SiteResponseModel> GetAllSitesByOrganisationId(Guid organisationId)
        {
            var sites = _siteRepository.GetAllSiteByOrganisationId(organisationId);
            //var response = _mapper.Map<List<SiteResponseModel>>(sites);
            return sites.Result;
        }
    }
}
