using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class LoanPlanRepository : GenericRepository<LoanPlan>, ILoanPlanRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanPlanRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }
    }
}
