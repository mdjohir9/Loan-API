using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class CustommerFinancialInfoRepository : GenericRepository<CustommerFinancialInfo>, ICustommerFinancialInfoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustommerFinancialInfoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public CustommerFinancialInfo GetCustommerFinancialByCustomerId(int customerId)
        {
            return _dbContext.CustommerFinancialInfo
                             .FirstOrDefault(c => c.CustomerID == customerId);
        }
    }
}
