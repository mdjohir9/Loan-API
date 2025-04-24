using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Loan_API.Implementation
{
    public class CustommerRepository: GenericRepository<CustommerPersonnelInfo>,ICustommerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CustommerRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
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
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            return await (from cpi in _dbContext.CustommerPersonnelInfo
                          join cc in _dbContext.CustommerContact on cpi.CustomerID equals cc.CustomerID into contactGroup
                          from cc in contactGroup.DefaultIfEmpty()
                          join ce in _dbContext.CustommerEmployment on cpi.CustomerID equals ce.CustomerID into employmentGroup
                          from ce in employmentGroup.DefaultIfEmpty()
                          join cfi in _dbContext.CustommerFinancialInfo on cpi.CustomerID equals cfi.CustomerID into financialGroup
                          from cfi in financialGroup.DefaultIfEmpty()
                          join cgd in _dbContext.CustommerGuarantorDetails on cpi.CustomerID equals cgd.CustomerID into guarantorGroup
                          from cgd in guarantorGroup.DefaultIfEmpty()
                          where (cpi.IsDeleted == false || cpi.IsDeleted == null)
                          orderby cpi.CustomerID descending
                          select new CustommerDetailesDTO
                          {
                              CustomerID=cpi.CustomerID,
                              CustCardNo = cpi.CustCardNo,
                              CompanyId = cpi.CompanyId,
                              CustommerImage = $"{baseUrl}/1111/CustommerImage/{cpi.CustommerImage}",
                              CustommerSignature = $"{baseUrl}/1111/CustommerSignature/{cpi.CustommerSignature}",
                              FullName = cpi.FullName,
                              Gender = cpi.Gender,
                              DateOfBirth = cpi.DateOfBirth,
                              Nationality = cpi.Nationality,
                              MaritalStatus = cpi.MaritalStatus,
                              Occupation = cpi.Occupation,
                              NationalIDOrPassport = cpi.NationalIDOrPassport,
                              TaxIdentificationNumber = cpi.TaxIdentificationNumber,
                              DrivingLicenseNumber = cpi.DrivingLicenseNumber,

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
        public async Task<IEnumerable<CustommerDetailesDTO>> GetAllWithDetailsAsync(int? customerId = null)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from cpi in _dbContext.CustommerPersonnelInfo
                        join cc in _dbContext.CustommerContact on cpi.CustomerID equals cc.CustomerID into contactGroup
                        from cc in contactGroup.DefaultIfEmpty()
                        join ce in _dbContext.CustommerEmployment on cpi.CustomerID equals ce.CustomerID into employmentGroup
                        from ce in employmentGroup.DefaultIfEmpty()
                        join cfi in _dbContext.CustommerFinancialInfo on cpi.CustomerID equals cfi.CustomerID into financialGroup
                        from cfi in financialGroup.DefaultIfEmpty()
                        join usr in _dbContext.Users on cpi.CustomerID.ToString() equals usr.ReferenceID into userGroup
                        from usr in userGroup.DefaultIfEmpty()
                        where customerId == null || cpi.CustomerID == customerId 
                        select new
                        {
                            cpi,
                            cc,
                            ce,
                            cfi,
                            usr,
                            
                        };

            var result = await query.ToListAsync(); // Fetch data from database first

            return result.Select(ti => new CustommerDetailesDTO
            {
                CustomerID=ti.cpi.CustomerID,
                Userid = ti.usr.UserId,
                CustCardNo = ti.cpi.CustCardNo,
                CompanyId = ti.cpi.CompanyId,
                CustommerImage = $"{baseUrl}/1111/CustommerImage/{ti.cpi.CustommerImage}",
                CustommerSignature = $"{baseUrl}/1111/CustommerSignature/{ti.cpi.CustommerSignature}",
                FullName = ti.cpi.FullName,
                Gender = ti.cpi.Gender,
                DateOfBirth = ti.cpi.DateOfBirth,
                Nationality = ti.cpi.Nationality,
                MaritalStatus = ti.cpi.MaritalStatus,
                Occupation = ti.cpi.Occupation,
                NationalIDOrPassport = ti.cpi.NationalIDOrPassport,
                TaxIdentificationNumber = ti.cpi.TaxIdentificationNumber,
                DrivingLicenseNumber = ti.cpi.DrivingLicenseNumber,
                EducationLevel=ti.cpi.EducationLevel,

                // Contact Information
                ContactId = ti.cc.ID,
                PhoneNumber = ti.cc?.PhoneNumber,
                AlternativePhoneNumber = ti.cc?.AlternativePhoneNumber,
                EmailAddress = ti.cc?.EmailAddress,
                PreStreet = ti.cc?.PreStreet,
                PerStreet = ti.cc?.PerStreet,
                PreZIP = ti.cc?.PreZIP,
                PerZIP = ti.cc?.PerZIP,
                PreCity = ti.cc?.PreCity,
                PerCity = ti.cc?.PerCity,
                PreState = ti.cc?.PreState,
                PerState = ti.cc?.PerState,

                // Employment Information
                EmploymentType = ti.ce?.EmploymentType,
                EmployerOrBusnName = ti.ce?.EmployerOrBusnName,
                JobTitleOrBusnType = ti.ce?.JobTitleOrBusnType,
                MonthlyIncOrBusnRev = ti.ce?.MonthlyIncOrBusnRev ?? 0m,
                YearsOfExpOrBusnAge = ti.ce?.YearsOfExpOrBusnAge ?? 0,
                WorkOrBusnAddress = ti.ce?.WorkOrBusnAddress,
                EmployerOrBusnContact = ti.ce?.EmployerOrBusnContact,

                // Financial Information
                BankName = ti.cfi?.BankName,
                AccountNumber = ti.cfi?.AccountNumber,
                MonthlyIncomeSources = ti.cfi?.MonthlyIncomeSources ?? 0m,
                MonthlyExpenses = ti.cfi?.MonthlyExpenses ?? 0m,
                AssetsOwned = ti.cfi?.AssetsOwned,
                Liabilities = ti.cfi?.Liabilities,
           
            }).ToList();
        }

        //public async Task<IEnumerable<CustommerDetailesDTO>> GetAllWithDetailsAsync()
        //{
        //    var request = _httpContextAccessor.HttpContext.Request;
        //    var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

        //    return await (from cpi in _dbContext.CustommerPersonnelInfo
        //                  join cc in _dbContext.CustommerContact on cpi.CustomerID equals cc.CustomerID into contactGroup
        //                  from cc in contactGroup.DefaultIfEmpty()
        //                  join ce in _dbContext.CustommerEmployment on cpi.CustomerID equals ce.CustomerID into employmentGroup
        //                  from ce in employmentGroup.DefaultIfEmpty()
        //                  join cfi in _dbContext.CustommerFinancialInfo on cpi.CustomerID equals cfi.CustomerID into financialGroup
        //                  from cfi in financialGroup.DefaultIfEmpty()
        //                  join cgd in _dbContext.CustommerGuarantorDetails on cpi.CustomerID equals cgd.CustomerID into guarantorGroup
        //                  from cgd in guarantorGroup.DefaultIfEmpty()
        //                  select new CustommerDetailesDTO
        //                  {
        //                      CustCardNo = cpi.CustCardNo,
        //                      CompanyId = cpi.CompanyId,
        //                      CustommerImage = $"{baseUrl}/0001/CustommerImage/{cpi.CustommerImage}",
        //                      CustommerSignature = $"{baseUrl}/0001/CustommerSignature/{cpi.CustommerSignature}",
        //                      FullName = cpi.FullName,
        //                      Gender = cpi.Gender,
        //                      DateOfBirth = cpi.DateOfBirth,
        //                      Nationality = cpi.Nationality,
        //                      MaritalStatus = cpi.MaritalStatus,
        //                      Occupation = cpi.Occupation,
        //                      NationalIDOrPassport= cpi.NationalIDOrPassport,
        //                      TaxIdentificationNumber = cpi.TaxIdentificationNumber,
        //                      DrivingLicenseNumber=cpi.DrivingLicenseNumber,

        //                      // Contact Information
        //                      PhoneNumber = cc != null ? cc.PhoneNumber : null,
        //                      AlternativePhoneNumber = cc != null ? cc.AlternativePhoneNumber : null,
        //                      EmailAddress = cc != null ? cc.EmailAddress : null,
        //                      PreStreet = cc != null ? cc.PreStreet : null,
        //                      PerStreet = cc != null ? cc.PerStreet : null,
        //                      PreZIP = cc != null ? cc.PreZIP : null,
        //                      PerZIP = cc != null ? cc.PerZIP : null,
        //                      PreCity = cc != null ? cc.PreCity : null, // Default value for nullable int
        //                      PerCity = cc != null ? cc.PerCity : null,
        //                      PreState = cc != null ? cc.PreState : null,
        //                      PerState = cc != null ? cc.PerState : null,

        //                      // Employment Information
        //                      EmploymentType = ce != null ? ce.EmploymentType : null,
        //                      EmployerOrBusnName = ce != null ? ce.EmployerOrBusnName : null,
        //                      JobTitleOrBusnType = ce != null ? ce.JobTitleOrBusnType : null,
        //                      MonthlyIncOrBusnRev = ce != null ? ce.MonthlyIncOrBusnRev : 0m, // Default value for decimal
        //                      YearsOfExpOrBusnAge = ce != null ? ce.YearsOfExpOrBusnAge : 0,
        //                      WorkOrBusnAddress = ce != null ? ce.WorkOrBusnAddress : null,
        //                      EmployerOrBusnContact = ce != null ? ce.EmployerOrBusnContact : null,

        //                      // Financial Information
        //                      BankName = cfi != null ? cfi.BankName : null,
        //                      AccountNumber = cfi != null ? cfi.AccountNumber : null,
        //                      MonthlyIncomeSources = cfi != null ? cfi.MonthlyIncomeSources : 0m, // Default for decimal
        //                      MonthlyExpenses = cfi != null ? cfi.MonthlyExpenses : 0m,
        //                      AssetsOwned = cfi != null ? cfi.AssetsOwned : null,
        //                      Liabilities = cfi != null ? cfi.Liabilities : null,

        //                      // Guarantor Details
        //                      GuarantorImage = cgd != null ? cgd.GuarantorImage : null,
        //                      GuarantorFullName = cgd != null ? cgd.GuarantorFullName : null,
        //                      RelationshipWithApplicant = cgd != null ? cgd.RelationshipWithApplicant : null,
        //                      GuarantorContactNumber = cgd != null ? cgd.GuarantorContactNumber : null,
        //                      GuarantorAddress = cgd != null ? cgd.GuarantorAddress : null,
        //                      GuarantorNationalIDOrPassport = cgd != null ? cgd.GuarantorNationalIDOrPassport : null,
        //                      GuarantorSignature = cgd != null ? cgd.GuarantorSignature : null
        //                  }).ToListAsync();

        //}

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

        public CustommerPersonnelInfo GetPersonnelInfoByCustomerId(int customerId)
        {
            return _dbContext.CustommerPersonnelInfo
                             .FirstOrDefault(c => c.CustomerID == customerId);
        }

        //public async Task<IEnumerable<CustommerIdAndNameDTO>> GetAllCustommerSummaryAsync(int? customerId)
        //{
        //    var customers = await _dbContext.CustommerPersonnelInfo
        //        .Select(c => new CustommerIdAndNameDTO
        //        {
        //            CustomerID = c.CustomerID,
        //            FullName = c.FullName
        //        })
        //        .ToListAsync();

        //    return customers;
        //}
        public async Task<IEnumerable<CustommerIdAndNameDTO>> GetAllCustommerSummaryAsync(int? customerId)
        {
            var query = _dbContext.CustommerPersonnelInfo.AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(c => c.CustomerID == customerId.Value && ( c.IsDeleted==null || c.IsDeleted==false));
            }
            else
            {
                query = query.Where(c => (c.IsDeleted == null || c.IsDeleted == false));

            }

            var customers = await query
                .Select(c => new CustommerIdAndNameDTO
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName
                })
                .ToListAsync();

            return customers;
        }
        private async Task<string> GenerateUniqueCustCardNoAsync()
        {
            var lastCustomer = await _dbContext.CustommerPersonnelInfo
                .OrderByDescending(c => c.CustCardNo)
                .FirstOrDefaultAsync();

            int nextNumber = 1001; // Default starting number

            if (lastCustomer != null && !string.IsNullOrEmpty(lastCustomer.CustCardNo))
            {
                string lastCardNo = lastCustomer.CustCardNo.Replace("UPS", "");
                if (int.TryParse(lastCardNo, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }
            
            return $"UPS{nextNumber}";
        }
        public async Task<string> SaveCustomerImageAsync(List<string> imageBase64List, string custCardNo, string companyId, string documentType)
        {
            if (imageBase64List == null || !imageBase64List.Any())
                return null;

            return await SaveDocumentsListsAsync(imageBase64List, custCardNo, companyId, documentType);
        }

        public async Task<int> AddCustommerAllDataAsync(CustommerSaveDTO dto)
        {

            try
            {
                var custCardNo = await GenerateUniqueCustCardNoAsync();
                string customerImage = await SaveCustomerImageAsync(dto.CustommerImage, custCardNo, "1111", "CustommerImage");


                // 1. Insert Personal Info and Save to get generated CustomerID
                var personalInfo = new CustommerPersonnelInfo
                {
                    CustommerImage= customerImage,
                    CustCardNo = custCardNo,
                    CompanyId = 1111,
                    FullName = dto.FullName,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    Nationality = dto.Nationality,
                    MaritalStatus = dto.MaritalStatus,
                    Occupation = dto.Occupation,
                    EducationLevel = dto.EducationLevel,
                    NationalIDOrPassport = dto.NationalIDOrPassport,
                    TaxIdentificationNumber = dto.TaxIdentificationNumber,
                    DrivingLicenseNumber = dto.DrivingLicenseNumber
                };

                _dbContext.CustommerPersonnelInfo.Add(personalInfo);
                await _dbContext.SaveChangesAsync();

                // 🔁 Return this ID to the caller
                int generatedCustomerID = personalInfo.CustomerID;

                var contact = new CustommerContact
                {
                    CustomerID = generatedCustomerID,
                    PhoneNumber = dto.PhoneNumber,
                    AlternativePhoneNumber = dto.AlternativePhoneNumber,
                    EmailAddress = dto.EmailAddress,
                    PreStreet = dto.PreStreet,
                    PerStreet = dto.PerStreet,
                    PreZIP = dto.PreZIP,
                    PerZIP = dto.PerZIP,
                    PreCity = dto.PreCity,
                    PerCity = dto.PerCity,
                    PreState = dto.PreState,
                    PerState = dto.PerState
                };

                var employment = new CustommerEmployment
                {
                    CustomerID = generatedCustomerID,
                    EmploymentType = dto.EmploymentType,
                    EmployerOrBusnName = dto.EmployerOrBusnName,
                    JobTitleOrBusnType = dto.JobTitleOrBusnType,
                    MonthlyIncOrBusnRev = dto.MonthlyIncOrBusnRev ?? 0m,
                    YearsOfExpOrBusnAge = dto.YearsOfExpOrBusnAge ?? 0,
                    WorkOrBusnAddress = dto.WorkOrBusnAddress,
                    EmployerOrBusnContact = dto.EmployerOrBusnContact
                };

                var financial = new CustommerFinancialInfo
                {
                    CustomerID = generatedCustomerID,
                    BankName = dto.BankName,
                    AccountNumber = dto.AccountNumber,
                    MonthlyIncomeSources = dto.MonthlyIncomeSources ?? 0m,
                    MonthlyExpenses = dto.MonthlyExpenses ?? 0m,
                    AssetsOwned = dto.AssetsOwned,
                    Liabilities = dto.Liabilities
                };

                _dbContext.CustommerContact.Add(contact);
                _dbContext.CustommerEmployment.Add(employment);
                _dbContext.CustommerFinancialInfo.Add(financial);

                await _dbContext.SaveChangesAsync();

                return generatedCustomerID;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task UpdateCustommerAllDataAsync(CustommerSaveDTO dto, string CustomerImage)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // Check if customer exists
                var personalInfo = await _dbContext.CustommerPersonnelInfo
                    .FirstOrDefaultAsync(x => x.CustomerID == dto.CustomerID);
                string customerImage = await SaveCustomerImageAsync(dto.CustommerImage, personalInfo.CustCardNo, "1111", "CustommerImage");


                if (personalInfo == null)
                    throw new Exception("Customer not found.");

                // Update Personal Info
                personalInfo.CustommerImage = customerImage;
                personalInfo.FullName = dto.FullName;
                personalInfo.Gender = dto.Gender;
                personalInfo.DateOfBirth = dto.DateOfBirth;
                personalInfo.Nationality = dto.Nationality;
                personalInfo.MaritalStatus = dto.MaritalStatus;
                personalInfo.Occupation = dto.Occupation;
                personalInfo.EducationLevel = dto.EducationLevel;
                personalInfo.NationalIDOrPassport = dto.NationalIDOrPassport;
                personalInfo.TaxIdentificationNumber = dto.TaxIdentificationNumber;
                personalInfo.DrivingLicenseNumber = dto.DrivingLicenseNumber;

                // Contact Info
                var contact = await _dbContext.CustommerContact
                    .FirstOrDefaultAsync(x => x.CustomerID == dto.CustomerID);

                if (contact != null)
                {
                    contact.PhoneNumber = dto.PhoneNumber;
                    contact.AlternativePhoneNumber = dto.AlternativePhoneNumber;
                    contact.EmailAddress = dto.EmailAddress;
                    contact.PreStreet = dto.PreStreet;
                    contact.PerStreet = dto.PerStreet;
                    contact.PreZIP = dto.PreZIP;
                    contact.PerZIP = dto.PerZIP;
                    contact.PreCity = dto.PreCity;
                    contact.PerCity = dto.PerCity;
                    contact.PreState = dto.PreState;
                    contact.PerState = dto.PerState;
                }

                // Employment Info
                var employment = await _dbContext.CustommerEmployment
                    .FirstOrDefaultAsync(x => x.CustomerID == dto.CustomerID);

                if (employment != null)
                {
                    employment.EmploymentType = dto.EmploymentType;
                    employment.EmployerOrBusnName = dto.EmployerOrBusnName;
                    employment.JobTitleOrBusnType = dto.JobTitleOrBusnType;
                    employment.MonthlyIncOrBusnRev = dto.MonthlyIncOrBusnRev ?? 0m;
                    employment.YearsOfExpOrBusnAge = dto.YearsOfExpOrBusnAge ?? 0;
                    employment.WorkOrBusnAddress = dto.WorkOrBusnAddress;
                    employment.EmployerOrBusnContact = dto.EmployerOrBusnContact;
                }

                // Financial Info
                var financial = await _dbContext.CustommerFinancialInfo
                    .FirstOrDefaultAsync(x => x.CustomerID == dto.CustomerID);

                if (financial != null)
                {
                    financial.BankName = dto.BankName;
                    financial.AccountNumber = dto.AccountNumber;
                    financial.MonthlyIncomeSources = dto.MonthlyIncomeSources ?? 0m;
                    financial.MonthlyExpenses = dto.MonthlyExpenses ?? 0m;
                    financial.AssetsOwned = dto.AssetsOwned;
                    financial.Liabilities = dto.Liabilities;
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
