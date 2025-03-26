using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class PersonnelInfoUpdateDTO
    {
        public List<string>? CustommerImage { get; set; } = new List<string>();
        public List<string>? CustommerSignature { get; set; } = new List<string>();

        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(200)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [MaxLength(100)]
        public string? Nationality { get; set; }
        [Required]
        public string? MaritalStatus { get; set; }
        public int? EducationLevel { get; set; }
        [MaxLength(100)]
        public string? Occupation { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "NationalIDOrPassport is required")]
        public string? NationalIDOrPassport { get; set; }


        public string? TaxIdentificationNumber { get; set; }

        public string? DrivingLicenseNumber { get; set; }
        public int? UserId { get; set; }
    }

}
