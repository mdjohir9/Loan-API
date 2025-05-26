using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class LoanRepository : GenericRepository<Loan>, IloanRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<LoanDetailesDTO>> GetAllLoanDetailsAsync()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var result = await (from la in _dbContext.Loan
                                join cpi in _dbContext.CustommerPersonnelInfo on la.CustomerID equals cpi.CustomerID into cpiJoin
                                from cpi in cpiJoin.DefaultIfEmpty()

                                join pm in _dbContext.PaymentMethod on la.PayMethodId equals pm.PayMethodID into pmJoin
                                from pm in pmJoin.DefaultIfEmpty()

                                join lp in _dbContext.LoanPlan on la.PlanID equals lp.PlanID into lpJoin
                                from lp in lpJoin.DefaultIfEmpty()
                                orderby la.LoanID descending
                                select new LoanDetailesDTO
                                {
                                    LoanId = la.LoanID,
                                    CustomerID = la.CustomerID,
                                    CustCardNo = cpi != null ? cpi.CustCardNo : null,
                                    FullName = cpi != null ? cpi.FullName : null,
                                    CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                                    Gender = cpi != null ? cpi.Gender : null,
                                    LoanAmount = la.LoanAmount,
                                    RepaymentPeriod = la.TenureMonths,
                                    MonthlyInstallments = la.MonthlyInstallment,
                                    // Strip the time part by using .Date to keep only the date
                                    DisbursementDate = la.DisbursementDate,
                                    Status = la.LoanStatus,
                                    PaymentMethodName = pm != null ? pm.Name : null,
                                    PlanName = lp != null ? lp.PlanName : null
                                }).ToListAsync();

            return result;
        }

        public async Task<List<LoanDetailesDTO>> GetLoanByCustomerDetailsAsync(int customerId)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var result = await (from la in _dbContext.Loan
                                join cpi in _dbContext.CustommerPersonnelInfo on la.CustomerID equals cpi.CustomerID into cpiJoin
                                from cpi in cpiJoin.DefaultIfEmpty()

                                join pm in _dbContext.PaymentMethod on la.PayMethodId equals pm.PayMethodID into pmJoin
                                from pm in pmJoin.DefaultIfEmpty()

                                join lp in _dbContext.LoanPlan on la.PlanID equals lp.PlanID into lpJoin
                                from lp in lpJoin.DefaultIfEmpty()
                                where la.CustomerID==customerId
                                orderby la.LoanID descending
                                select new LoanDetailesDTO
                                {
                                    LoanId = la.LoanID,
                                    CustomerID = la.CustomerID,
                                    CustCardNo = cpi != null ? cpi.CustCardNo : null,
                                    FullName = cpi != null ? cpi.FullName : null,
                                    CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                                    Gender = cpi != null ? cpi.Gender : null,
                                    LoanAmount = la.LoanAmount,
                                    RepaymentPeriod = la.TenureMonths,
                                    MonthlyInstallments = la.MonthlyInstallment,
                                    // Strip the time part by using .Date to keep only the date
                                    DisbursementDate = la.DisbursementDate,
                                    Status = la.LoanStatus,
                                    PaymentMethodName = pm != null ? pm.Name : null,
                                    PlanName = lp != null ? lp.PlanName : null
                                }).ToListAsync();

            return result;
        }

        public async Task<LoanStatementDTO> GetLoanDetailsByIdAsync(int loanId)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var result = await (from la in _dbContext.Loan
                                join cpi in _dbContext.CustommerPersonnelInfo on la.CustomerID equals cpi.CustomerID into cpiJoin
                                from cpi in cpiJoin.DefaultIfEmpty()
                                join pm in _dbContext.PaymentMethod on la.PayMethodId equals pm.PayMethodID into pmJoin
                                from pm in pmJoin.DefaultIfEmpty()
                                join lp in _dbContext.LoanPlan on la.PlanID equals lp.PlanID into lpJoin
                                from lp in lpJoin.DefaultIfEmpty()
                                where la.LoanID == loanId
                                select new LoanStatementDTO
                                {
                                    LoanId = la.LoanID,
                                    CustomerID = la.CustomerID,
                                    CustCardNo = cpi != null ? cpi.CustCardNo : null,
                                    FullName = cpi != null ? cpi.FullName : null,
                                    CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                                    Gender = cpi != null ? cpi.Gender : null,
                                    LoanAmount = la.LoanAmount,
                                    RepaymentPeriod = la.TenureMonths,
                                    MonthlyInstallments = la.MonthlyInstallment,
                                    DisbursementDate = la.DisbursementDate,
                                    Status = la.LoanStatus,
                                    PaymentMethodName = pm != null ? pm.Name : null,
                                    PlanName = lp != null ? lp.PlanName : null,
                                    CompanyLogo = $"{baseUrl}/1111/CompanyDocuments/logo.png",
                                    CompanyName = "Upstart Loan",
                                    BankLogo = $"{baseUrl}/1111/CompanyDocuments/bankLogo.jpg",
                                    AuthorizeSignature = $"{baseUrl}/1111/CompanyDocuments/authorizeSignature.png",
                                    Approvelogo = $"{baseUrl}/1111/CompanyDocuments/approvelogo.jpg",
                                   

                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<LoanBalanceDto> GetLoanBalanceByCustomerIdAsync(int customerId)
        {
            return await (from ab in _dbContext.AccountBalance
                          join ln in _dbContext.Loan
                              .Where(l => l.LoanStatus == 1) // Filter only active loans
                              on ab.CustomerId equals ln.CustomerID into gj
                          from ln in gj.DefaultIfEmpty()
                          where ab.CustomerId == customerId
                          select new LoanBalanceDto
                          {
                              BalanceAmount = ab.BalanceAmount,
                              LoanAmount = ln != null ? ln.LoanAmount : 0,
                              DueAmount = ln != null ? ln.DueAmount : 0,
                              MonthlyInstallment = ln != null ? ln.MonthlyInstallment ?? 0 : 0
                          }).FirstOrDefaultAsync();
        }


    }
}
