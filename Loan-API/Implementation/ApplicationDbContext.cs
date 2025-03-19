using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Loan_API.Implementation
{
    public class ApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        //{
        //    _configuration = configuration;

        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}
    }
}
