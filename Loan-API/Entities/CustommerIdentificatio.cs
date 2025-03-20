using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class CustommerIdentificatio
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string? NationalIDOrPassport { get; set; }

        public string? TaxIdentificationNumber { get; set; }

        public string? DrivingLicenseNumber { get; set; }
    }
}
