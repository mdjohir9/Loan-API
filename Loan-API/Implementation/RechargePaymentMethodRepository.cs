using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<RechargePaymentMethod>> GetAllActiveAsync()
        {
            return await _dbContext.RechargePaymentMethod
                .Where(p => p.IsActive == true)
                .ToListAsync();
        }

    }
}
