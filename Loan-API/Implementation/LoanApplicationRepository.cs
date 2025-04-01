using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class LoanApplicationRepository: GenericRepository<LoanApplication>, ILoanApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanApplicationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }
    }
}
