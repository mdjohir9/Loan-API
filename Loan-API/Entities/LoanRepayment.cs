using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanRepayment
    {
        [Key]
        public int RepaymentID { get; set; }

        [Required]
        public int LoanID { get; set; }
        [ForeignKey("LoanID")]
        public LoanApplication? Loan { get; set; }

        [Required]
        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string? PaymentMethod { get; set; }  // Bank Transfer, Cash, Mobile Payment

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";  // Pending, Completed, Failed
    }
}
