using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IRechargePaymentMethodRepository : IGenericRepository<RechargePaymentMethod>
    {
        Task<IEnumerable<RechargePaymentMethod>> GetAllActiveAsync();

    }
}
