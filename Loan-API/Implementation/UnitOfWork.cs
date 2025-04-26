using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
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

            Custommer = new CustommerRepository(_dbContext , _httpContextAccessor);
            Contact = new CustommerContactRepository(_dbContext);
            Employment = new CustommerEmploymentRepository(_dbContext);
            FinancialInfo = new CustommerFinancialInfoRepository (_dbContext);
            Guarantor = new CustommerGuarantorRepository(_dbContext);
            User = new UserRepository(_dbContext, _httpContextAccessor);
            UserRole = new UserRoleRepository(_dbContext);
            Login = new LoginRepository(_dbContext, _configuration);
            LoanApplication = new LoanApplicationRepository(_dbContext, _httpContextAccessor);
            Loan = new LoanRepository(_dbContext, _httpContextAccessor);
            LoanInstalment = new LoanInstalmentRepository(_dbContext ,_httpContextAccessor);
            LoanPlan = new LoanPlanRepository(_dbContext);
            Transction = new TransctionRepository(_dbContext);

            Account= new AccountRepository(_dbContext);
            RechargeAccount = new RechargeAccountRepository(_dbContext);
            RechargePaymentMethod= new RechargePaymentMethodRepository(_dbContext);
            Recharge = new RechargeRepository(_dbContext);
            Withdraw = new WithdrawRepository(_dbContext, _httpContextAccessor);
        }

        public ICustommerRepository Custommer { get; private set; }
        public ICustommerContactRepository Contact { get; private set; }
        public ICustommerEmploymentRepository Employment { get; private set; }
        public ICustommerFinancialInfoRepository FinancialInfo { get; private set; }
        public ICustommerGuarantorRepository Guarantor { get; private set; }
        public IUserRepository User { get; private set; }
        public IUserRoleRepository UserRole { get; private set; }
   
        public ILoginRepository Login { get; private set; }
        public ILoanApplicationRepository LoanApplication { get; private set; }
        public IloanRepository Loan { get; private set; }
        public ILoanInstalmentRepository LoanInstalment { get; private set; }
        public ILoanPlanRepository LoanPlan { get; private set; }
        public ITransctionRepository Transction { get; private set; }
        public IAccountRepository Account { get; private set; }
        public IRechargePaymentMethodRepository RechargePaymentMethod { get; private set; }
        public IRechargeAccountRepository RechargeAccount { get; private set; }
        public IRechargeRepository Recharge { get; private set; }

        public IWithdrawRepository Withdraw { get; private set; }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
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
