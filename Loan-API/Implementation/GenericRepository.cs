using Loan_API.Repository;
using System.Collections.Generic;

namespace Loan_API.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        //protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            //_dbSet = _dbContext.Set<T>();

        }

    }
}
