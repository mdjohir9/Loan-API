using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class CustommerContactRepository : GenericRepository<CustommerContact>, ICustommerContactRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustommerContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public CustommerContact GetCustommerContactByCustomerId(int customerId)
        {
            return _dbContext.CustommerContact
                             .FirstOrDefault(c => c.CustomerID == customerId);
        }
    }
}
