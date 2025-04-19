using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class CustommerSaveDTO
    {
        public int? CustomerID { get; set; }
        public string? CustCardNo { get; set; }
        public int CompanyId { get; set; }
        public List<string>? CustommerImage { get; set; } = new List<string>();
        public List<string>? CustommerSignature { get; set; } = new List<string>();
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Occupation { get; set; }
        public int? EducationLevel { get; set; }
        public string? NationalIDOrPassport { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? DrivingLicenseNumber { get; set; }

        // Contact Info
        public string? PhoneNumber { get; set; }
        public string? AlternativePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? PreStreet { get; set; }
        public string? PerStreet { get; set; }
        public string? PreZIP { get; set; }
        public string? PerZIP { get; set; }
        public string? PreCity { get; set; }
        public string? PerCity { get; set; }
        public string? PreState { get; set; }
        public string? PerState { get; set; }

        // Employment Info
        public string? EmploymentType { get; set; }
        public string? EmployerOrBusnName { get; set; }
        public string? JobTitleOrBusnType { get; set; }
        public decimal? MonthlyIncOrBusnRev { get; set; }
        public int? YearsOfExpOrBusnAge { get; set; }
        public string? WorkOrBusnAddress { get; set; }
        public string? EmployerOrBusnContact { get; set; }

        // Financial Info
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? MonthlyIncomeSources { get; set; }
        public decimal? MonthlyExpenses { get; set; }
        public string? AssetsOwned { get; set; }
        public string? Liabilities { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
