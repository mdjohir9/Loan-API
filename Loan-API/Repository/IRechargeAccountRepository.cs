using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IRechargeAccountRepository : IGenericRepository<RechargeAccount>
    {
        List<RechargeAccount> GetRechargeAccountsByPaymentType(int recPaymentMethodId);
        Task<IEnumerable<object>> GetRechargeAccountsAsync();

    }
}
