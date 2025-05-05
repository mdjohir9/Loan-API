using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    [SoftDelete]
    public class LoanPlan
    {
        [Key]
        public int PlanID { get; set; }

        [Required]
        [MaxLength(150)]
        public string? PlanName { get; set; }

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

        public decimal ProcessingFee { get; set; } = 0;

        public decimal LatePaymentPenalty { get; set; } = 0;

        public string? Descraption { get; set; }

        [Required]
        public byte? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
    }
}
