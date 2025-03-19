using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Collateral
    {
        [Key]
        public int CollateralID { get; set; }

        [Required]
        public int LoanID { get; set; }
        [ForeignKey("LoanID")]
        public LoanApplication? Loan { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CollateralType { get; set; }  // e.g., Car, Property

        [Required]
        public decimal EstimatedValue { get; set; }

        public string? DocumentReference { get; set; }  // File path if document is stored

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Submitted";
    }
}
