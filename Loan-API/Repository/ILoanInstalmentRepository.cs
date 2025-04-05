using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface ILoanInstalmentRepository : IGenericRepository<LoanInstalment>
    {
        Task AddRangeAsync(IEnumerable<LoanInstalment> instalments);
        Task<IEnumerable<LoanInstalmentDetailsDTO>> GetInstalmentDetailsByIdAsync(int id);

    }
}
