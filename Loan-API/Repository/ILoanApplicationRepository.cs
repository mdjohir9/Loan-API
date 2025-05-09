using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface ILoanApplicationRepository : IGenericRepository<LoanApplication>
    {
        Task<List<LoanApplicationDetailesDTO>> GetAllLoanApplicationsWithDetailsAsync();
        Task<List<LoanApplicationDetailesDTO>> GetLoanApplicationsByCustomerIdAsync(int customerId);
        Task<LoanLimitDTO?> GetLoanLimitsByPlanIdAsync(int planId);

    }
}
