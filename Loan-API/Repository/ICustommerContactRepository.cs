using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface ICustommerContactRepository: IGenericRepository<CustommerContact>
    {
        CustommerContact GetCustommerContactByCustomerId(int customerId);

    }
}
