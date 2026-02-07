using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Loan_API.Implementation
{
    public class LoanInstalmentRepository : GenericRepository<LoanInstalment>, ILoanInstalmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanInstalmentRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddRangeAsync(IEnumerable<LoanInstalment> instalments)
        {
            await _dbContext.LoanInstalment.AddRangeAsync(instalments);
        }
        public async Task<IEnumerable<LoanInstalmentDetailsDTO>> GetInstalmentDetailsByIdAsync(int id)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from li in _dbContext.LoanInstalment
                        join l in _dbContext.Loan on li.LoanID equals l.LoanID
                        join cpi in _dbContext.CustommerPersonnelInfo on l.CustomerID equals cpi.CustomerID
                        join lp in _dbContext.LoanPlan on l.PlanID equals lp.PlanID
                        join pm in _dbContext.PaymentMethod on li.PayMethodId equals pm.PayMethodID into pmGroup
            from pm in pmGroup.DefaultIfEmpty()
                        where l.LoanID == id orderby li.Status ascending
                        select new LoanInstalmentDetailsDTO
                        {
                            FullName = cpi.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                            CustCardNo = cpi.CustCardNo,
                            InstalmentID = li.InstalmentID,
                            LoanID = li.LoanID,
                            PlanID = lp.PlanID,
                            PlanName = lp.PlanName,
                            PaymentDate = li.PaymentDate,
                            Status = li.Status,
                            AmountPaid = li.AmountPaid,
                            LateCharge=li.LateCharge,
                            PayMethodName = pm != null ? pm.Name : null,
                            LoanNumber = l.LoanNumber,
                            LoanAmount = l.LoanAmount,
                            DueAmount = l.DueAmount,
                            PaidAmount = l.PaidAmount,
                            LoanStartDate = l.LoanStartDate,
                            LoanEndDate = l.LoanEndDate,
                            TotalPayableAmount = l.TotalPayableAmount
                        };

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<LoanInstalmentDetailsDTO>> GetInstalmentsByMonthAsync(DateTime date)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from li in _dbContext.LoanInstalment
                        join l in _dbContext.Loan on li.LoanID equals l.LoanID
                        join cpi in _dbContext.CustommerPersonnelInfo on l.CustomerID equals cpi.CustomerID
                        join lp in _dbContext.LoanPlan on l.PlanID equals lp.PlanID
                        join pm in _dbContext.PaymentMethod on li.PayMethodId equals pm.PayMethodID into pmGroup
                        from pm in pmGroup.DefaultIfEmpty()
                        where li.PaymentDate.Month == date.Month && li.PaymentDate.Year == date.Year
                        select new LoanInstalmentDetailsDTO
                        {
                            FullName = cpi.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                            CustCardNo = cpi.CustCardNo,
                            InstalmentID = li.InstalmentID,
                            LoanID = li.LoanID,
                            PlanID = lp.PlanID,
                            PlanName = lp.PlanName,
                            PaymentDate = li.PaymentDate,
                            Status = li.Status,
                            AmountPaid = li.AmountPaid,
                            PayMethodName = pm != null ? pm.Name : null,
                            LoanNumber = l.LoanNumber,
                            LoanAmount = l.LoanAmount,
                            DueAmount = l.DueAmount,
                            PaidAmount = l.PaidAmount,
                            LateCharge=li.LateCharge,
                            LoanStartDate = l.LoanStartDate,
                            LoanEndDate = l.LoanEndDate,
                            TotalPayableAmount = l.TotalPayableAmount
                        };

            return await query.ToListAsync();
        }


    }

}
