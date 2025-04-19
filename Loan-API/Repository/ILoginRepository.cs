using Loan_API.DTO;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface ILoginRepository : IGenericRepository<User>
    {
        IEnumerable<Entities.User> GetLoginInfo(string userName, string userPassword);
        //string GetUserDepartment(string EmpId);
        //string GetUserDesignation(string EmpId);
        //IEnumerable<string> GetUserPermission(string userId);
        UserProfileDTO GetUserProfileInfo(int Id);
        CompanyStatusDTO GetUserCompany(int userId);
        Loan GetLoanInformation(int customerId);

        string GenerateJwtToken(User user);
    }
}
