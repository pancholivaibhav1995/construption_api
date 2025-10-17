using AutoMapper;
using Construction.Core.Construct;
using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using Construction.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.Core.Concrete
{
    public class LabourPaymentService : ILabourPaymentService
    {
        private readonly ILabourPaymentRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISiteRepository _siteRepository;

        public LabourPaymentService(ILabourPaymentRepository repo, IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository, ISiteRepository siteRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _siteRepository = siteRepository;
        }

        public async Task<List<LabourPaymentResponseModel>> GetAllAsync(Guid organisationId)
        {
            var list = await _repo.GetAllByOrganisationAsync(organisationId);
            return _mapper.Map<List<LabourPaymentResponseModel>>(list);
        }

        public async Task<LabourPaymentResponseModel> AddAsync(LabourPaymentRequestModel request)
        {
            if (request == null) throw new ArgumentException("Invalid request");

            // validate overlap per site
            var paymentsOnSite = await _repo.GetPaymentsBySiteAsync(request.SiteId);
            if (paymentsOnSite.Any(p => PeriodsOverlap(p.PaymentPeriodStartDate, p.PaymentPeriodEndDate, request.PaymentPeriodStartDate, request.PaymentPeriodEndDate)))
            {
                throw new ArgumentException("Payment period overlaps with an existing payment for this site.");
            }

            var entity = _mapper.Map<LabourPayment>(request);
            entity.LabourPaymentId = Guid.NewGuid();
            entity.CreatedDate = DateTime.UtcNow;
            entity.UserId = request.UserId;

            await _repo.AddAsync(entity);
            await _repo.CommitAsync();

            return _mapper.Map<LabourPaymentResponseModel>(entity);
        }

        public async Task<LabourPaymentResponseModel> UpdateAsync(LabourPaymentRequestModel request)
        {
            if (request == null || request.LabourPaymentId == Guid.Empty) throw new ArgumentException("Invalid request");

            var existing = await _repo.GetAsyncById(request.LabourPaymentId);
            if (existing == null) throw new KeyNotFoundException("Labour payment not found");

            // validate overlap: exclude current record
            var paymentsOnSite = await _repo.GetPaymentsBySiteAsync(request.SiteId);
            if (paymentsOnSite.Any(p => p.LabourPaymentId != request.LabourPaymentId && PeriodsOverlap(p.PaymentPeriodStartDate, p.PaymentPeriodEndDate, request.PaymentPeriodStartDate, request.PaymentPeriodEndDate)))
            {
                throw new ArgumentException("Payment period overlaps with an existing payment for this site.");
            }

            existing.Amount = request.Amount;
            existing.Notes = request.Notes;
            existing.SiteId = request.SiteId;
            existing.PaymentPeriodStartDate = request.PaymentPeriodStartDate;
            existing.PaymentPeriodEndDate = request.PaymentPeriodEndDate;
            existing.UserId = request.UserId;
            existing.UpdatedDate = DateTime.UtcNow;

            await _repo.CommitAsync();

            return _mapper.Map<LabourPaymentResponseModel>(existing);
        }

        private bool PeriodsOverlap(DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {
            // consider inclusive overlap
            return aStart <= bEnd && bStart <= aEnd;
        }

        public async Task<List<UserManagerResponseModel>> GetEmployeesWithLabourAccessAsync(Guid organisationId)
        {
            var roles = await _roleRepository.GetAllAsync(organisationId);
            var labourRoleIds = roles
                .Where(r => r.Rolename != null && (r.Rolename.Contains("Labour", StringComparison.OrdinalIgnoreCase) || r.Rolename.Contains("Labourer", StringComparison.OrdinalIgnoreCase)))
                .Select(r => r.Roleid)
                .ToList();

            if (!labourRoleIds.Any())
                return new List<UserManagerResponseModel>();

            var users = await _userRepository.GetAllUsersByOrganisationAsync(organisationId);

            var result = users.Where(u => labourRoleIds.Contains(u.Roleid)).ToList();
            return result;
        }

        public async Task<LabourSiteDataResponseModel> GetLabourEmployeesAndSitesAsync(Guid organisationId)
        {
            // determine labour roles
            var roles = await _roleRepository.GetAllAsync(organisationId);
            var labourRoleIds = roles
                .Where(r => r.Rolename != null && (r.Rolename.Contains("Labour", StringComparison.OrdinalIgnoreCase) || r.Rolename.Contains("Labourer", StringComparison.OrdinalIgnoreCase)))
                .Select(r => r.Roleid)
                .ToList();

            List<UserManagerResponseModel> employees = new List<UserManagerResponseModel>();
            if (labourRoleIds.Any())
            {
                var users = await _userRepository.GetAllUsersByOrganisationAsync(organisationId);
                employees = users.Where(u => labourRoleIds.Contains(u.Roleid)).ToList();
            }

            var sites = await _siteRepository.GetAllSiteByOrganisationId(organisationId);

            var result = new LabourSiteDataResponseModel
            {
                Employees = employees.Select(u => new LabourEmployeeResponseModel
                {
                    Userid = u.Userid,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Roleid = u.Roleid,
                    Wageperday = u.Wageperday
                }).ToList(),
                Sites = sites
            };

            return result;
        }
    }
}