using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Loan_API.Implementation
{
    public class TransctionRepository : GenericRepository<Transaction>, ITransctionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransctionRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
   
        }
        public async Task<IEnumerable<TransctionDetailesDTO>> GetTransactionsByCustomerAndDateRangeAsync(int customerId, DateTime fromDate, DateTime toDate)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            var result = await (from txs in _dbContext.Transaction
                                join cpi in _dbContext.CustommerPersonnelInfo on txs.CustomerId equals cpi.CustomerID into cpiJoin
                                from cpi in cpiJoin.DefaultIfEmpty()
                                join typ in _dbContext.TransactionType on txs.TransactionType equals typ.TransactionTypeID into typJoin
                                from typ in typJoin.DefaultIfEmpty()
                                join pm in _dbContext.PaymentMethod on txs.PaytMethodID equals pm.PayMethodID into pmJoin
                                from pm in pmJoin.DefaultIfEmpty()
                                where txs.CustomerId == customerId
                                      && txs.TransactionDate.Date >= fromDate.Date
                                      && txs.TransactionDate.Date <= toDate.Date
                                select new TransctionDetailesDTO
                                {
                                    CustomerId = txs.CustomerId,
                                    CustommerImage =  $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",

                                    FullName = cpi.FullName,
                                    CustCardNo = cpi.CustCardNo,
                                    TransactionType = typ.Name,
                                    Amount = txs.Amount,
                                    TransactionDate = txs.TransactionDate,
                                    PaymentMethod = pm.Name,
                                    Remarks = txs.Remarks
                                }).ToListAsync();

            return result;
        }

        public async Task<object> GetAdminDashboardSummaryAsync()
        {
            var totalCustomers = await _dbContext.CustommerPersonnelInfo
                .Where(c => c.IsDeleted == false || c.IsDeleted == null)
                .CountAsync();

            var totalActiveLoans = await _dbContext.Loan
                .Where(l => l.LoanStatus == 1)
                .CountAsync();

            var disbursementAmount = await _dbContext.Transaction
                .Where(t => t.TransactionType == 1)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            var repaymentAmount = await _dbContext.Transaction
                .Where(t => t.TransactionType == 2)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            return new
            {
                TotalCustomers = totalCustomers,
                TotalActiveLoan = totalActiveLoans,
                DisbursementAmount = disbursementAmount,
                RepaymentAmount = repaymentAmount
            };
        }
        public async Task<object> GetrepaymentAndDisbursedSummaryAsync(int year)
        {
            var disbursementAmount = await _dbContext.Transaction
                .Where(t => t.TransactionType == 1 && t.TransactionDate.Year == year)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            var repaymentAmount = await _dbContext.Transaction
                .Where(t => t.TransactionType == 2 && t.TransactionDate.Year == year)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            // Monthly Repayment Totals
            var monthlyRepayments = await _dbContext.Transaction
                .Where(t => t.TransactionType == 2 && t.TransactionDate.Year == year)
                .GroupBy(t => t.TransactionDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            // Monthly Disbursement Totals
            var monthlyDisbursements = await _dbContext.Transaction
                .Where(t => t.TransactionType == 1 && t.TransactionDate.Year == year)
                .GroupBy(t => t.TransactionDate.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            // Combine both into 12-month arrays
            var monthlyRepaymentData = Enumerable.Range(1, 12)
                .Select(month =>
                {
                    var match = monthlyRepayments.FirstOrDefault(x => x.Month == month);
                    return match != null ? match.Total : 0;
                })
                .ToList();

            var monthlyDisbursementData = Enumerable.Range(1, 12)
                .Select(month =>
                {
                    var match = monthlyDisbursements.FirstOrDefault(x => x.Month == month);
                    return match != null ? match.Total : 0;
                })
                .ToList();

            return new
            {
                Year = year,
                DisbursementAmount = disbursementAmount,
                RepaymentAmount = repaymentAmount,
                MonthlyRepaymentAmounts = monthlyRepaymentData,
                MonthlyDisbursementAmounts = monthlyDisbursementData
            };
        }



    }
}
