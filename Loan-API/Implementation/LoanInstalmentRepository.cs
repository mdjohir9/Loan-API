using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class LoanInstalmentRepository : GenericRepository<LoanInstalment>, ILoanInstalmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanInstalmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task AddRangeAsync(IEnumerable<LoanInstalment> instalments)
        {
            await _dbContext.LoanInstalment.AddRangeAsync(instalments);
        }
        public async Task<IEnumerable<LoanInstalmentDetailsDTO>> GetInstalmentDetailsByIdAsync(int id)
        {
            var query = from li in _dbContext.LoanInstalment
                        join l in _dbContext.Loan on li.LoanID equals l.LoanID
                        join lp in _dbContext.LoanPlan on l.PlanID equals lp.PlanID
                        join pm in _dbContext.PaymentMethod on li.PayMethodId equals pm.PayMethodID into pmGroup
                        from pm in pmGroup.DefaultIfEmpty()
                        where l.LoanID == id
                        select new LoanInstalmentDetailsDTO
                        {
                            InstalmentID = li.InstalmentID,
                            LoanID = li.LoanID,
                            PlanID = lp.PlanID,
                            PlanName = lp.PlanName,
                            PaymentDate = li.PaymentDate,
                            Status = li.Status,
                            AmountPaid = li.AmountPaid,
                            PayMethodName = pm.Name,
                            LoanNumber = l.LoanNumber,
                            LoanAmount = l.LoanAmount,
                            DueAmount = l.DueAmount,
                            PaidAmount=l.PaidAmount,
                            LoanStartDate = l.LoanStartDate,
                            LoanEndDate = l.LoanEndDate,
                            TotalPayableAmount = l.TotalPayableAmount
                        };

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<LoanInstalmentDetailsDTO>> GetInstalmentsByMonthAsync(DateTime date)
        {
            var query = from li in _dbContext.LoanInstalment
                        join l in _dbContext.Loan on li.LoanID equals l.LoanID
                        join lp in _dbContext.LoanPlan on l.PlanID equals lp.PlanID
                        join pm in _dbContext.PaymentMethod on li.PayMethodId equals pm.PayMethodID into pmGroup
                        from pm in pmGroup.DefaultIfEmpty()
                        where li.PaymentDate.Month == date.Month && li.PaymentDate.Year == date.Year
                        select new LoanInstalmentDetailsDTO
                        {
                            InstalmentID = li.InstalmentID,
                            LoanID = li.LoanID,
                            PlanID = lp.PlanID,
                            PlanName = lp.PlanName,
                            PaymentDate = li.PaymentDate,
                            Status = li.Status,
                            AmountPaid = li.AmountPaid,
                            PayMethodName = pm.Name,
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

    }

}
