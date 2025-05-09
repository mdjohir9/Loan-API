using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class LoanApplicationRepository: GenericRepository<LoanApplication>, ILoanApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanApplicationRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<LoanApplicationDetailesDTO>> GetAllLoanApplicationsWithDetailsAsync()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var result = await (from la in _dbContext.LoanApplication
                                join cpi in _dbContext.CustommerPersonnelInfo on la.CustomerID equals cpi.CustomerID into cpiJoin
                                from cpi in cpiJoin.DefaultIfEmpty()

                                join pm in _dbContext.PaymentMethod on la.PayMethodID equals pm.PayMethodID into pmJoin
                                from pm in pmJoin.DefaultIfEmpty()

                                join lp in _dbContext.LoanPlan on la.PlanID equals lp.PlanID into lpJoin
                                from lp in lpJoin.DefaultIfEmpty()
                                orderby la.ApplicationID descending
                                select new LoanApplicationDetailesDTO
                                {
                                    ApplicationID=la.ApplicationID,   
                                    CustomerID = la.CustomerID,
                                    CustCardNo = cpi != null ? cpi.CustCardNo : null,
                                    FullName = cpi != null ? cpi.FullName : null,
                                    CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                                    Gender = cpi != null ? cpi.Gender : null,
                                    LoanAmount = la.LoanAmount,
                                    RepaymentPeriod = la.RepaymentPeriod,
                                    MonthlyInstallments = la.MonthlyInstallments,
                                    // Strip the time part by using .Date to keep only the date
                                    ApplicationDate = la.ApplicationDate.Date,
                                    PurposeOfLoan = la.PurposeOfLoan,
                                    Status = la.Status,
                                    PaymentMethodName = pm != null ? pm.Name : null,
                                    PlanName = lp != null ? lp.PlanName : null
                                }).ToListAsync();

            return result;
        }
        public async Task<List<LoanApplicationDetailesDTO>> GetLoanApplicationsByCustomerIdAsync(int customerId)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var result = await (from la in _dbContext.LoanApplication
                                where la.CustomerID == customerId
                                join cpi in _dbContext.CustommerPersonnelInfo on la.CustomerID equals cpi.CustomerID into cpiJoin
                                from cpi in cpiJoin.DefaultIfEmpty()

                                join pm in _dbContext.PaymentMethod on la.PayMethodID equals pm.PayMethodID into pmJoin
                                from pm in pmJoin.DefaultIfEmpty()

                                join lp in _dbContext.LoanPlan on la.PlanID equals lp.PlanID into lpJoin
                                from lp in lpJoin.DefaultIfEmpty()
                                orderby la.ApplicationID descending
                                select new LoanApplicationDetailesDTO
                                {
                                    ApplicationID = la.ApplicationID,
                                    CustomerID = la.CustomerID,
                                    CustCardNo = cpi != null ? cpi.CustCardNo : null,
                                    FullName = cpi != null ? cpi.FullName : null,
                                    CustommerImage = cpi != null ? $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}" : null,
                                    Gender = cpi != null ? cpi.Gender : null,
                                    LoanAmount = la.LoanAmount,
                                    RepaymentPeriod = la.RepaymentPeriod,
                                    MonthlyInstallments = la.MonthlyInstallments,
                                    ApplicationDate = la.ApplicationDate.Date,
                                    PurposeOfLoan = la.PurposeOfLoan,
                                    Status = la.Status,
                                    PaymentMethodName = pm != null ? pm.Name : null,
                                    PlanName = lp != null ? lp.PlanName : null
                                }).ToListAsync();

            return result;
        }

        public async Task<LoanLimitDTO?> GetLoanLimitsByPlanIdAsync(int planId)
        {
            var result = await _dbContext.LoanPlan
                .Where(p => p.PlanID == planId && p.IsActive == 1 && p.Deleted != true)
                .Select(p => new LoanLimitDTO
                {
                    MinAmount = p.MinAmount,
                    MaxAmount = p.MaxAmount,
                    MinRepaymentPeriod = p.MinRepaymentPeriod,
                    MaxRepaymentPeriod = p.MaxRepaymentPeriod
                })
                .FirstOrDefaultAsync();

            return result; // Returns null if not found
        }




    }
}
