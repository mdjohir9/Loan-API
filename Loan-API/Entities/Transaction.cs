using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Transaction
    {
        [Key]
        public int TransctionID { get; set; }

        [Required]
        public int TransactionType { get; set; }  // e.g., Loan Disbursement, Repayment

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

 
        [Required]
        public int PaytMethodID { get; set; }


        public string? Remarks { get; set; } 
    }
}
