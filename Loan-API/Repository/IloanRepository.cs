using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IloanRepository : IGenericRepository<Loan>
    {
        Task<List<LoanDetailesDTO>> GetAllLoanDetailsAsync();
        Task<LoanStatementDTO> GetLoanDetailsByIdAsync(int loanId);

    }
}
