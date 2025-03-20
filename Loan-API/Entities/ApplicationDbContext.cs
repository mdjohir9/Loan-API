using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Loan_API.Entities
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CustommerPersonnelInfo> CustommerPersonnelInfo { get; set; }
    }
}
