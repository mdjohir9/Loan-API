using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface ITransctionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<TransctionDetailesDTO>> GetTransactionsByCustomerAndDateRangeAsync(int customerId, DateTime fromDate, DateTime toDate);
        Task<object> GetAdminDashboardSummaryAsync();
        Task<object> GetrepaymentAndDisbursedSummaryAsync(int year);


    }
}
