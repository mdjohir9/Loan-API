using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.Extensions.Options;

namespace Loan_API.Implementation
{
    public class UnitOfWork:IUnitOfWork
    {
        //private readonly ApplicationDbContext _dbContext;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        ////private IGenericRepository<Users> _usersRepository;
        ////private IGenericRepository<UserModule> _userModuleRepository;
        //private readonly IConfiguration _configuration;
        //private readonly string _connectionString;
        //public UnitOfWork(ApplicationDbContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        //{
        //    _dbContext = dbContext;
        //    _configuration = configuration;
        //    _httpContextAccessor = httpContextAccessor;
        //    _connectionString = _configuration.GetConnectionString("DefaultConnection");
        //}


        //public void Dispose()
        //{
        //    _dbContext.Dispose();
        //}

        //public Task<int> Save()
        //{
        //    return _dbContext.SaveChangesAsync();
        //}
    }
}
