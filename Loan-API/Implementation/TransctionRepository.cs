using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class TransctionRepository : GenericRepository<Transaction>, ITransctionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public TransctionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            //_httpContextAccessor = httpContextAccessor;
        }
    }
}
