using Loan_API.Entities;
using Loan_API.Repository;

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
    }

}
