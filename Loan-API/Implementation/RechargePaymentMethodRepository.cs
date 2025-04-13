using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class RechargePaymentMethodRepository : GenericRepository<RechargePaymentMethod>, IRechargePaymentMethodRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public RechargePaymentMethodRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }


    }
}
