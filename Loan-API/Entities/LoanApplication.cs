using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanApplication
    {
        [Key]
        public int LoanID { get; set; }

        [Required]
        public int CustomerID { get; set; }  // Foreign Key (Assuming Customers table exists)

        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public LoanProduct? LoanProduct { get; set; }

        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int RepaymentPeriod { get; set; } // In months

        [MaxLength(255)]
        public string? Purpose { get; set; }

        public string? CollateralDetails { get; set; }  // If applicable

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";  // Pending, Approved, Rejected, Disbursed, Closed

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        public DateTime? DisbursementDate { get; set; }
    }
}
