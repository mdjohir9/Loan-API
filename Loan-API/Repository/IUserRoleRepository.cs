using Loan_API.DTO.UserRoles;
using Loan_API.Entities;

namespace Loan_API.Repository
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        IEnumerable<UserRole> GetUserRoleByIdCustom(int Id);
        Task<IEnumerable<DdlRolesDTO>> GetUserRolesAsync(bool IsGuestUser, string CompanyId);
        Task<IEnumerable<UserRolesInfoDTO>> GelAllUserRolesAsync(string companyId, bool IsAdministrator);
    }
}
