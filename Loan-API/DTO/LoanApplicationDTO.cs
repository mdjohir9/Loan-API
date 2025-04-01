using Loan_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class LoanApplicationDTO
    {
   


        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public int RepaymentPeriod { get; set; } // In months

        [Required]
        [MaxLength(255)]
        public string? PurposeOfLoan { get; set; }

        public decimal? MonthlyInstallments { get; set; }

        [Required]
        public byte Status { get; set; } //0= unpaid 1=paid  

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;


        public int? ApplyedBy { get; set; }
        public int? ApprovedBy { get; set; }
        public int? RejectedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [Required]
        public int PlanID { get; set; }
        [Required]

        public int CustomerID { get; set; }
        [Required]
        public int? PayMethodID { get; set; }

    }
}
