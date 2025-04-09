using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Withdraw
    {
        [Key]
        public int WithdrawaID { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int PaymentMethodID { get; set; }

        [ForeignKey(nameof(PaymentMethodID))]
        public PaymentMethod PaymentMethod { get; set; } = null!;

        [Required]
        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        [StringLength(100)]
        public string? TransactionCode { get; set; } 

        [StringLength(500)]
        public string? AdminRemarks { get; set; }

        public DateTime? ProcessedDate { get; set; }

        [StringLength(50)]
        public string? ProcessedBy { get; set; }
    }
}
