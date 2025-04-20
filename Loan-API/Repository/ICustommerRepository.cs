using Loan_API.DTO;
using Loan_API.Entities;
using System.Linq.Expressions;

namespace Loan_API.Repository
{
    public interface ICustommerRepository: IGenericRepository<CustommerPersonnelInfo>
    {
        Task<int> AddCustomerAsync(CustommerPersonnelInfoDTO customerDto, int userId);
        Task<IEnumerable<CustommerDetailesDTO>> GetAllWithDetailsAsync(int? CustommerId);
        Task<IEnumerable<CustommerDetailesDTO>> GetAllWithDetailsAsync();
        Task<IEnumerable<CustommerIdAndNameDTO>> GetAllCustommerSummaryAsync(int? CustommerId);

        Task<int> AddCustomerContactAsync(CustommerContactDTO ContactDo, int userId);
        Task<int> AddCustomerEmploymentAsync(CustommerEmploymentDTO employmentDto, int userId);
        Task<int> AddCustomerFinancialInfoAsync(CustommerFinancialInfoDTO financialInfoDto, int userId);
        Task<int> AddCustomerGuarantorAsync(CustommerGuarantorDetailsDTO GuarantorDto, int userId);
        CustommerPersonnelInfo GetPersonnelInfoByCustomerId(int customerId);
        Task<int> AddCustommerAllDataAsync(CustommerSaveDTO dto);

        Task UpdateCustommerAllDataAsync(CustommerSaveDTO dto);


    }
}
