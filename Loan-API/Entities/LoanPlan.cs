using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanPlan
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public int LoanTypeID { get; set; }  // Foreign Key
        [ForeignKey("LoanTypeID")]
        public LoanType? LoanType { get; set; }

        [Required]
        [MaxLength(150)]
        public string? ProductName { get; set; }

        [Required]
        public decimal MinAmount { get; set; }

        [Required]
        public decimal MaxAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; } // Percentage

        [Required]
        public int MinRepaymentPeriod { get; set; } // In months

        [Required]
        public int MaxRepaymentPeriod { get; set; }

        public bool CollateralRequired { get; set; } = false;

        public decimal ProcessingFee { get; set; } = 0;

        public decimal LatePaymentPenalty { get; set; } = 0;
    }
}
