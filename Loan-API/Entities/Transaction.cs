using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Transaction
    {
        [Key]
        public int TransctionID { get; set; }

        [Required]
        public string TransactionType { get; set; } = null!; // e.g., Loan Disbursement, Repayment

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        // Foreign key to Customer
        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public CustommerPersonnelInfo Customer { get; set; } = null!;

        // Foreign key to Loan (if applicable)
        public int? LoanID { get; set; }

        [ForeignKey(nameof(LoanID))]
        public Loan? Loan { get; set; }

        // Foreign key to PaymentMethod
        [Required]
        public int PaytMethodID { get; set; }

        [ForeignKey(nameof(PaytMethodID))]
        public PaymentMethod PaymentMethod { get; set; } = null!;

        public string? Remarks { get; set; } 
    }
}
