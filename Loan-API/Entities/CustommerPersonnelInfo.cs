using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class CustommerPersonnelInfo
    {
        [Key]
        [Required]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "CustCardNo is required")]
        public string? CustCardNo { get; set; }
        [Required(ErrorMessage = "CompanyId is required")]
        public int? CompanyId { get; set; }

        public string? CustommerImage { get; set; } // Stores the image as binary data

        public string ? CustommerSignature { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(200)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; } // Male, Female, Other

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [MaxLength(100)]
        public string? Nationality { get; set; }
        public string? MaritalStatus { get; set; } // Single, Married, Divorced

        public int? EducationLevel { get; set; } // High School, Bachelor, Master, PhD

        [MaxLength(100)]
        public string? Occupation { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public Boolean ? IsDeleted { get; set; }
        public CustommerContact? CustommerContact { get; set; }
        public CustommerIdentificatio? CustommerIdentificatio { get; set; }
        public CustommerEmployment? CustommerEmployment { get; set; }
        public CustommerFinancialInfo? CustommerFinancialInfo { get; set; }
        public CustommerGuarantorDetails? CustommerGuarantorDetails { get; set; }
    }
}
