using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IAccountRepository : IGenericRepository<AccountBalance> 
    {
        Task<int> GenerateUniqueAccountNumberAsync();
        AccountBalance GetAccountInfoCustomerId(int customerId);
    }
}
