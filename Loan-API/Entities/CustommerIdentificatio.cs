using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual CustommerPersonnelInfo? CustommerPersonnelInfo { get; set; }
    }
}
