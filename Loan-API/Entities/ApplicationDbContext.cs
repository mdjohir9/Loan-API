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
        public DbSet <Recharge> Recharge { get; set; }
        public DbSet<Withdraw> Withdraw { get; set; }

        public DbSet <RechargePaymentMethod> RechargePaymentMethod { get; set; }
        public DbSet <RechargeAccount> RechargeAccount { get; set; }
        public DbSet <TblCountry> TblCountry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
             .IsUnique(); // Makes the UserName column unique

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustommerPersonnelInfo>()
        .HasOne(e => e.CustommerEmployment)
        .WithOne(e => e.CustommerPersonnelInfo)
        .HasForeignKey<CustommerEmployment>(e => e.CustomerID)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustommerPersonnelInfo>()
                .HasOne(c => c.CustommerContact)
                .WithOne(c => c.CustommerPersonnelInfo)
                .HasForeignKey<CustommerContact>(c => c.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustommerPersonnelInfo>()
                .HasOne(f => f.CustommerFinancialInfo)
                .WithOne(f => f.CustommerPersonnelInfo)
                .HasForeignKey<CustommerFinancialInfo>(f => f.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustommerPersonnelInfo>()
                .HasOne(g => g.CustommerGuarantorDetails)
                .WithOne(g => g.CustommerPersonnelInfo)
                .HasForeignKey<CustommerGuarantorDetails>(g => g.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);
        }




    }
}
