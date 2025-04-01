using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanApplication
    {
        [Key]
        public int ApplicationID { get; set; }


        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public int RepaymentPeriod { get; set; } // In months

        [Required]
        [MaxLength(255)]
        public string? PurposeOfLoan { get; set; }


        public bool HasExistingLoans { get; set; } = false;

        public decimal? ExistingLoanAmount { get; set; }

        public string? LenderName { get; set; }

        public decimal? MonthlyInstallments { get; set; }

        [Required]
        public byte Status { get; set; } //0=pending, 1=approve, 2=Reject 

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        public DateTime? ApplyedAt { get; set; }
        public int? ApplyedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? RejectAt { get; set; }
        public int? RejectedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }


        [Required]
        public int PlanID { get; set; }
        [ForeignKey(nameof(PlanID))]
        public LoanPlan? LoanPlan { get; set; } = null!;

        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public CustommerPersonnelInfo Customer { get; set; }
        public int? PayMethodID { get; set; }
        [ForeignKey("PayMethodID")]
        public virtual PaymentMethod? PaymentMethod { get; set; }

        
    }
}
