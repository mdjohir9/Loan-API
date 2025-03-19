using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class PersonnelIdentificationDetails
    {
        [Key]
        public int IdentificationID { get; set; }

        [Required]
        public string? NationalIDOrPassport { get; set; }

        public string? TaxIdentificationNumber { get; set; }

        public string? DrivingLicenseNumber { get; set; }
    }
}
