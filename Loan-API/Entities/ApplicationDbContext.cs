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
        public DbSet<CustommerIdentificatio> CustommerIdentificatio { get; set; }
        public DbSet<CustommerGuarantorDetails> CustommerGuarantorDetails { get; set; }
        public DbSet<CustommerFinancialInfo> CustommerFinancialInfo { get; set; }
        public DbSet<CustommerEmployment> CustommerEmployment { get; set; }
        public DbSet<CustommerContact> CustommerContact { get; set; }
    }
}
