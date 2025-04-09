using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Deposit
    {
        [Required]
        [Key]
        public int DepositID { get; set; }

        [Required]
        [StringLength(20)]
        public string BankAccountNumber { get; set; } = null!; // this is a Owaner Bank Account Number Whwere Brower menualy Transfer Money

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; } = false;

        [StringLength(100)]
        public string? BankTransactCode { get; set; } // Manual statement/transaction reference

        [StringLength(500)]
        public string? AdminRemarks { get; set; }

        public DateTime? ProcessedDate { get; set; }

        [StringLength(50)]
        public string? ProcessedBy { get; set; }

        [Required]
        public int PaymentMethodID { get; set; }


        [Required]
        public int CustommerID {  get; set; }

        [ForeignKey(nameof(CustommerID))]
        public CustommerPersonnelInfo CustommerPersonnelInfo { get; set; } = null!;



    }
}
