using Loan_API.Entities;
using Loan_API.Repository;

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


    }
}
