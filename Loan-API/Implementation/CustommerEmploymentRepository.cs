using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class CustommerEmploymentRepository : GenericRepository<CustommerEmployment>, ICustommerEmploymentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustommerEmploymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
