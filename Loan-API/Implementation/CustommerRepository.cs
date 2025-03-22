using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class CustommerRepository: GenericRepository<CustommerPersonnelInfo>,ICustommerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustommerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddCustomerAsync(CustommerPersonnelInfoDTO customerDto, int userId)
        {
            var customer = new CustommerPersonnelInfo
            {
                CustCardNo = customerDto.CustCardNo,
                CompanyId = customerDto.CompanyId,
                FullName = customerDto.FullName,
                Gender = customerDto.Gender,
                DateOfBirth = customerDto.DateOfBirth,
                Nationality = customerDto.Nationality,
                MaritalStatus = customerDto.MaritalStatus,
                Occupation = customerDto.Occupation,
                CreatedAt = DateTime.Now,
                CreatedBy = userId,
                IsActive = true
            };

           
            await _dbContext.CustommerPersonnelInfo.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer.CustomerID;
           
        }

        public async Task<int> AddCustomerContactAsync(CustommerContactDTO ContactDto, int customerId)
        {
            var contact = new CustommerContact
            {
                CustomerID = customerId,
                PhoneNumber = ContactDto.PhoneNumber,
                AlternativePhoneNumber = ContactDto.AlternativePhoneNumber,
                EmailAddress = ContactDto.EmailAddress,
                PreStreet = ContactDto.PreStreet,
                PerStreet = ContactDto.PerStreet,
                PreZIP = ContactDto.PreZIP,
                PerZIP = ContactDto.PerZIP,
                PreCity = ContactDto.PreCity,
                PerCity = ContactDto.PerCity,
                PreState = ContactDto.PreState,
                PerState = ContactDto.PerState
            };

         
             await _dbContext.CustommerContact.AddAsync(contact);
             await _dbContext.SaveChangesAsync();
             return contact.ID;
         
        }
        public async Task<IEnumerable<CustommerDetailesDTO>> GetAllWithDetailsAsync()
        {
            return await (from cpi in _dbContext.CustommerPersonnelInfo
                          join cc in _dbContext.CustommerContact on cpi.CustomerID equals cc.CustomerID into contactGroup
                          from cc in contactGroup.DefaultIfEmpty()
                          join ce in _dbContext.CustommerEmployment on cpi.CustomerID equals ce.CustomerID into employmentGroup
                          from ce in employmentGroup.DefaultIfEmpty()
                          join cfi in _dbContext.CustommerFinancialInfo on cpi.CustomerID equals cfi.CustomerID into financialGroup
                          from cfi in financialGroup.DefaultIfEmpty()
                          join cgd in _dbContext.CustommerGuarantorDetails on cpi.CustomerID equals cgd.CustomerID into guarantorGroup
                          from cgd in guarantorGroup.DefaultIfEmpty()
                          select new CustommerDetailesDTO
                          {
                              CustCardNo = cpi.CustCardNo,
                              CompanyId = cpi.CompanyId,
                              CustommerImage = cpi.CustommerImage,
                              CustommerSignature = cpi.CustommerSignature,
                              FullName = cpi.FullName,
                              Gender = cpi.Gender,
                              DateOfBirth = cpi.DateOfBirth,
                              Nationality = cpi.Nationality,
                              MaritalStatus = cpi.MaritalStatus,
                              Occupation = cpi.Occupation,
                              NationalIDOrPassport= cpi.NationalIDOrPassport,
                              TaxIdentificationNumber = cpi.TaxIdentificationNumber,
                              DrivingLicenseNumber=cpi.DrivingLicenseNumber,

                              // Contact Information
                              PhoneNumber = cc != null ? cc.PhoneNumber : null,
                              AlternativePhoneNumber = cc != null ? cc.AlternativePhoneNumber : null,
                              EmailAddress = cc != null ? cc.EmailAddress : null,
                              PreStreet = cc != null ? cc.PreStreet : null,
                              PerStreet = cc != null ? cc.PerStreet : null,
                              PreZIP = cc != null ? cc.PreZIP : null,
                              PerZIP = cc != null ? cc.PerZIP : null,
                              PreCity = cc != null ? cc.PreCity : null, // Default value for nullable int
                              PerCity = cc != null ? cc.PerCity : null,
                              PreState = cc != null ? cc.PreState : null,
                              PerState = cc != null ? cc.PerState : null,

                              // Employment Information
                              EmploymentType = ce != null ? ce.EmploymentType : null,
                              EmployerOrBusnName = ce != null ? ce.EmployerOrBusnName : null,
                              JobTitleOrBusnType = ce != null ? ce.JobTitleOrBusnType : null,
                              MonthlyIncOrBusnRev = ce != null ? ce.MonthlyIncOrBusnRev : 0m, // Default value for decimal
                              YearsOfExpOrBusnAge = ce != null ? ce.YearsOfExpOrBusnAge : 0,
                              WorkOrBusnAddress = ce != null ? ce.WorkOrBusnAddress : null,
                              EmployerOrBusnContact = ce != null ? ce.EmployerOrBusnContact : null,

                              // Financial Information
                              BankName = cfi != null ? cfi.BankName : null,
                              AccountNumber = cfi != null ? cfi.AccountNumber : null,
                              MonthlyIncomeSources = cfi != null ? cfi.MonthlyIncomeSources : 0m, // Default for decimal
                              MonthlyExpenses = cfi != null ? cfi.MonthlyExpenses : 0m,
                              AssetsOwned = cfi != null ? cfi.AssetsOwned : null,
                              Liabilities = cfi != null ? cfi.Liabilities : null,

                              // Guarantor Details
                              GuarantorImage = cgd != null ? cgd.GuarantorImage : null,
                              GuarantorFullName = cgd != null ? cgd.GuarantorFullName : null,
                              RelationshipWithApplicant = cgd != null ? cgd.RelationshipWithApplicant : null,
                              GuarantorContactNumber = cgd != null ? cgd.GuarantorContactNumber : null,
                              GuarantorAddress = cgd != null ? cgd.GuarantorAddress : null,
                              GuarantorNationalIDOrPassport = cgd != null ? cgd.GuarantorNationalIDOrPassport : null,
                              GuarantorSignature = cgd != null ? cgd.GuarantorSignature : null
                          }).ToListAsync();

        }

        public async Task<int> AddCustomerEmploymentAsync(CustommerEmploymentDTO employmentDto, int customerId)
        {
            var employment = new CustommerEmployment
            {
                CustomerID = customerId,
                EmploymentType = employmentDto.EmploymentType,
                EmployerOrBusnName = employmentDto.EmployerOrBusnName,
                JobTitleOrBusnType = employmentDto.JobTitleOrBusnType,
                MonthlyIncOrBusnRev = employmentDto.MonthlyIncOrBusnRev,
                YearsOfExpOrBusnAge = employmentDto.YearsOfExpOrBusnAge,
                WorkOrBusnAddress = employmentDto.WorkOrBusnAddress,
                EmployerOrBusnContact = employmentDto.EmployerOrBusnContact
            };

            
            await _dbContext.CustommerEmployment.AddAsync(employment);
            await _dbContext.SaveChangesAsync();
            return employment.ID;
            
            
        }

        public async Task<int> AddCustomerFinancialInfoAsync(CustommerFinancialInfoDTO financialInfoDTO, int customerId)
        {
            var financialInfo = new CustommerFinancialInfo
            {
                CustomerID = customerId,
                BankName = financialInfoDTO.BankName,
                AccountNumber = financialInfoDTO.AccountNumber,
                MonthlyIncomeSources = financialInfoDTO.MonthlyIncomeSources,
                MonthlyExpenses = financialInfoDTO.MonthlyExpenses,
                AssetsOwned = financialInfoDTO.AssetsOwned,
                Liabilities = financialInfoDTO.Liabilities
            };

         
             await _dbContext.CustommerFinancialInfo.AddAsync(financialInfo);
             await _dbContext.SaveChangesAsync();
             return financialInfo.ID;
            
          
        }

        public async Task<int> AddCustomerGuarantorAsync(CustommerGuarantorDetailsDTO GuarantorDto, int customerId)
        {
            var guarantor = new CustommerGuarantorDetails
            {
                CustomerID = customerId,
                GuarantorImage = GuarantorDto.GuarantorImage,
                GuarantorFullName = GuarantorDto.GuarantorFullName,
                RelationshipWithApplicant = GuarantorDto.RelationshipWithApplicant,
                GuarantorContactNumber = GuarantorDto.GuarantorContactNumber,
                GuarantorAddress = GuarantorDto.GuarantorAddress,
                GuarantorNationalIDOrPassport = GuarantorDto.GuarantorNationalIDOrPassport,
                GuarantorSignature = GuarantorDto.GuarantorSignature
            };

        
            
            await _dbContext.CustommerGuarantorDetails.AddAsync(guarantor);
            await _dbContext.SaveChangesAsync();
            return guarantor.ID;
            
            
        }
 

    }
}
