using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IRechargeRepository : IGenericRepository<Recharge>
    {
        Task<List<RechargeDetailDTO>> GetAllRechargeDetailsAsync();

    }
}
