using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.Extensions.Options;

namespace Loan_API.Implementation
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

      
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public UnitOfWork(ApplicationDbContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _connectionString = _configuration.GetConnectionString("DbConnection");

            Custommer = new CustommerRepository(_dbContext);
            Contact = new CustommerContactRepository(_dbContext);
            Employment = new CustommerEmploymentRepository(_dbContext);
            FinancialInfo = new CustommerFinancialInfoRepository (_dbContext);
            Guarantor = new CustommerGuarantorRepository(_dbContext);

        }

        public ICustommerRepository Custommer { get; private set; }
        public ICustommerContactRepository Contact { get; private set; }
        public ICustommerEmploymentRepository Employment { get; private set; }
        public ICustommerFinancialInfoRepository FinancialInfo { get; private set; }
        public ICustommerGuarantorRepository Guarantor { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task<int> Save()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
