using Loan_API.Entities;
using Loan_API.Repository;

namespace Loan_API.Implementation
{
    public class CustommerGuarantorRepository : GenericRepository<CustommerGuarantorDetails>, ICustommerGuarantorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustommerGuarantorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
