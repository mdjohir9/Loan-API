using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface ICustommerEmploymentRepository : IGenericRepository<CustommerEmployment>
    {

        CustommerEmployment GetCustommerEmploymentByCustomerId(int customerId);
    }
}
