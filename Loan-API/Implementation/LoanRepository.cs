using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class LoanRepository : GenericRepository<Loan>, IloanRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }
    }
}
