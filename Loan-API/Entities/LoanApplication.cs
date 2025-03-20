using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanApplication
    {
        [Key]
        public int LoanID { get; set; }

        // Foreign Key - Customer
        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public CustommerPersonnelInfo Customer { get; set; }

        // Foreign Key - Loan Plan/Product
        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public LoanPlan LoanPlan { get; set; }

        // Loan Details
        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int RepaymentPeriod { get; set; } // In months

        [Required]
        [MaxLength(255)]
        public string Purpose { get; set; }

        public string? CollateralDetails { get; set; }  // If applicable

        // Repayment and Loan History
        [Required]
        [MaxLength(50)]
        public string PreferredRepaymentMethod { get; set; } // Bank Transfer, Cash, Mobile Payment

        public bool HasExistingLoans { get; set; } = false;

        public decimal? ExistingLoanAmount { get; set; }

        public string? LenderName { get; set; }

        public decimal? MonthlyInstallments { get; set; }

        // Loan Status and Timestamps
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";  // Pending, Approved, Rejected, Disbursed, Closed

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        public DateTime? DisbursementDate { get; set; }
    }
}
