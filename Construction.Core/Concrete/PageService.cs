using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Construction.Core.Concrete
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;

        public PageService(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<List<PageResponseModel>> GetAllAsync(Guid organisationId)
        {
            var pages = await _pageRepository.GetAllByOrganisationAsync(organisationId);
            return _mapper.Map<List<PageResponseModel>>(pages);
        }

        public async Task<PageResponseModel> AddAsync(PageRequestModel request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PageName))
                throw new ArgumentException("Invalid request");

            var page = _mapper.Map<Page>(request);
            page.PageId = request.PageId == Guid.Empty ? Guid.NewGuid() : request.PageId;
            page.CreatedDate = DateTime.UtcNow;

            await _pageRepository.AddAsync(page);
            await _pageRepository.CommitAsync();

            return _mapper.Map<PageResponseModel>(page);
        }

        public async Task<PageResponseModel> UpdateAsync(PageRequestModel request)
        {
            if (request == null || request.PageId == Guid.Empty)
                throw new ArgumentException("Invalid request");

            var page = await _pageRepository.GetAsyncById(request.PageId);
            if (page == null) throw new KeyNotFoundException("Page not found");

            page.PageName = request.PageName;
            page.UpdatedDate = DateTime.UtcNow;

            await _pageRepository.CommitAsync();

            return _mapper.Map<PageResponseModel>(page);
        }
    }
}