using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class RechargeAccountRepository : GenericRepository<RechargeAccount>, IRechargeAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public RechargeAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }

        public List<RechargeAccount> GetRechargeAccountsByPaymentType(int recPaymentMethodId)
        {
            return _dbContext.RechargeAccount
                             .Where(c => c.RecPaymentMethodId == recPaymentMethodId)
                             .ToList();
        }

        public async Task<IEnumerable<object>> GetRechargeAccountsAsync()
        {
            var result = await (from ra in _dbContext.RechargeAccount
                                join rpm in _dbContext.RechargePaymentMethod
                                    on ra.RecPaymentMethodId equals rpm.Id into gj
                                from rpm in gj.DefaultIfEmpty()
                                select new
                                {
                                    ra.Id,
                                    RecPaymentMethod = rpm != null ? rpm.Name : null,
                                    ra.BankOrWalletName,
                                    ra.AccountName,
                                    ra.AccountNumber,
                                    ra.IsActive
                                }).ToListAsync();

            return result;
        }

    }
}
