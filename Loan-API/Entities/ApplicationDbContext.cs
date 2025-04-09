using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Loan_API.Entities
{
    public class ApplicationDbContext:DbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CustommerPersonnelInfo> CustommerPersonnelInfo { get; set; }
        public DbSet<CustommerGuarantorDetails> CustommerGuarantorDetails { get; set; }
        public DbSet<CustommerFinancialInfo> CustommerFinancialInfo { get; set; }
        public DbSet<CustommerEmployment> CustommerEmployment { get; set; }
        public DbSet<CustommerContact> CustommerContact { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HrdCompanyInfo> HrdCompanyInfo { get; set; }
        public DbSet<LoanPlan> LoanPlan { get; set; }
        public DbSet<LoanApplication> LoanApplication { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<LoanInstalment> LoanInstalment { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<AccountBalance> AccountBalance { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet <Deposit> Deposits { get; set; }
        public DbSet <RechargePaymentMethod> RechargePaymentMethod { get; set; }
        public DbSet <RechargeAccount> RechargeAccount { get; set; }



    }
}
