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


        public async Task<LoanDetailesDTO> GetLoanDetailsByIdAsync(int loanId)
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
                                    DisbursementDate = la.DisbursementDate,
                                    Status = la.LoanStatus,
                                    PaymentMethodName = pm != null ? pm.Name : null,
                                    PlanName = lp != null ? lp.PlanName : null
                                }).FirstOrDefaultAsync();

            return result;
        }
    }
}
