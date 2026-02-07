using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IWithdrawRepository : IGenericRepository<Withdraw>
    {
   
        Task<List<WithdrawDetailDTO>> GetAllWithdrawDetailsAsync();
        Task<List<WithdrawDetailDTO>> GetWithdrawDetailsByCustomerIdAsync(int customerId);

    }
}
