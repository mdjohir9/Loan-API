using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Recharge
    {
        [Required]
        [Key]
        public int RechargeID { get; set; }

        public string? BankAccountNumber { get; set; } = null!; 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;

        public bool? IsApproved { get; set; }

        [StringLength(100)]
        public string? BankTransactCode { get; set; } // Manual statement/transaction reference

        [StringLength(500)]
        public string? AdminRemarks { get; set; }

        public string? Statement { get; set; }

        public DateTime? ApproveAt { get; set; }

        [StringLength(50)]
        public int? ApproveBy { get; set; }
        public DateTime? RejectAt { get; set; }

        [StringLength(50)]
        public int? RejectBy { get; set; }
        public int? ApplyedBy { get; set; }
        public DateTime? ApplyedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }


        [Required]
        public int PaymentMethodID { get; set; }//this is Owaner Part
        [Required]
        public int BankId { get; set; } // this is a Owaner Bank Account Number Whwere Brower menualy Transfer Money


        [Required]
        public int CustommerID {  get; set; }

        [ForeignKey(nameof(CustommerID))]
        public CustommerPersonnelInfo CustommerPersonnelInfo { get; set; } = null!;



    }
}
