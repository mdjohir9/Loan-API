using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanPlanCreateDTO
    {
        [Required]
        [MaxLength(250)]
        public string? PlanName { get; set; }

        [Required]
        public decimal MinAmount { get; set; }

        [Required]
        public decimal MaxAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public int MinRepaymentPeriod { get; set; }

        [Required]
        public int MaxRepaymentPeriod { get; set; }

        public decimal ProcessingFee { get; set; } = 0;

        public decimal LatePaymentPenalty { get; set; } = 0;

        public string? Descraption { get; set; }

        [Required]
        public byte IsActive { get; set; }
    }
}
