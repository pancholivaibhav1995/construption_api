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
    protected readonly IReceiptRepository _receiptRepository;
    protected readonly ILabourPaymentRepository _labourPaymentRepository;
    protected readonly ISiteTransactionRepository _siteTransactionRepository;
    protected readonly IMapper _mapper;

    public DashboardService(
        IDashboardRepository dashboardRepository,
        ISiteRepository siteRepository,
        IUserRepository userRepository,
        IReceiptRepository receiptRepository,
        ILabourPaymentRepository labourPaymentRepository,
        ISiteTransactionRepository siteTransactionRepository,
        IMapper mapper)
    {
        _dashboardRepository = dashboardRepository;
        _siteRepository = siteRepository;
        _userRepository = userRepository;
        _receiptRepository = receiptRepository;
        _labourPaymentRepository = labourPaymentRepository;
        _siteTransactionRepository = siteTransactionRepository;
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

    public async Task<DashboardCountsResponseModel> GetDashboardCountsAsync(Guid organisationId, Guid? siteId = null)
    {
        // Sites counts
        var allSites = _siteRepository.GetAll();
        var totalActiveSites = allSites.Count(s => s.Organisationid == organisationId && s.Isactive == true);
        //var totalCompletedSites = allSites.Count(s => s.Organisationid == organisationId && s.Isactive == false);

        // Employees
        var allUsers = await _userRepository.GetAllUsersByOrganisationAsync(organisationId);
        var totalEmployees = allUsers.Count();

        // Labour payments total (paid)
        decimal totalLabourPaymentPaid = 0m;
        if (siteId.HasValue && siteId.Value != Guid.Empty)
        {
            var payments = await _labourPaymentRepository.GetPaymentsBySiteAsync(siteId.Value);
            totalLabourPaymentPaid = payments.Sum(p => p.Amount);
        }
        else
        {
            var payments = await _labourPaymentRepository.GetAllByOrganisationAsync(organisationId);
            totalLabourPaymentPaid = payments.Sum(p => p.Amount);
        }

        // Supplier receipts: pending and done
        var receiptList = (await _receiptRepository.GetAllAsync(organisationId)).ToList();
        if (siteId.HasValue && siteId.Value != Guid.Empty)
            receiptList = receiptList.Where(r => r.SiteId == siteId.Value).ToList();

        // Trim PaymentStatus values to remove padding
        receiptList.ForEach(r => r.PaymentStatus = r.PaymentStatus?.Trim());

        // PaymentStatus classification
        var pendingStatuses = new[] { "pending", "unpaid", "due" };
        var doneStatuses = new[] { "paid", "done", "completed" };

        decimal totalSupplierPaymentPending = receiptList
            .Where(r => !string.IsNullOrWhiteSpace(r.PaymentStatus) && pendingStatuses.Contains(r.PaymentStatus, StringComparer.OrdinalIgnoreCase))
            .Sum(r => r.Amount);

        decimal totalSupplierPaymentDone = receiptList
            .Where(r => !string.IsNullOrWhiteSpace(r.PaymentStatus) && doneStatuses.Contains(r.PaymentStatus, StringComparer.OrdinalIgnoreCase))
            .Sum(r => r.Amount);

        // Site transactions: debit and credit totals
        var transactions = await _siteTransactionRepository.GetAllByOrganisationAsync(organisationId);
        if (siteId.HasValue && siteId.Value != Guid.Empty)
            transactions = transactions.Where(t => t.SourceId == siteId.Value).ToList();

        // Normalize TransactionType to remove trailing/leading spaces
        transactions.ForEach(t => t.TransactionType = t.TransactionType?.Trim());

        decimal totalCreditTransactions = transactions
            .Where(t => !string.IsNullOrWhiteSpace(t.TransactionType) && string.Equals(t.TransactionType, "CREDIT", StringComparison.OrdinalIgnoreCase))
            .Sum(t => t.Amount);

        decimal totalDebitTransactions = transactions
            .Where(t => !string.IsNullOrWhiteSpace(t.TransactionType) && string.Equals(t.TransactionType, "DEBIT", StringComparison.OrdinalIgnoreCase))
            .Sum(t => t.Amount);

        // Derived totals
        decimal totalIncome = totalCreditTransactions;
        decimal totalExpense = totalLabourPaymentPaid + totalSupplierPaymentPending + totalSupplierPaymentDone;

        return new DashboardCountsResponseModel
        {
            TotalActiveSites = totalActiveSites,
           // TotalCompletedSites = totalCompletedSites,
            TotalEmployees = totalEmployees,
            TotalLabourPaymentPaid = totalLabourPaymentPaid,
            TotalSupplierPaymentPending = totalSupplierPaymentPending,
            TotalSupplierPaymentDone = totalSupplierPaymentDone,
            TotalCreditTransactions = totalCreditTransactions,
            TotalDebitTransactions = totalDebitTransactions,
            TotalIncome = totalIncome,
            TotalExpense = totalExpense
        };
    }
}