using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanInstalment
    {
        [Key]
        public int InstalmentID { get; set; }

        [Required]
        public int LoanID { get; set; }
        [ForeignKey("LoanID")]
        public Loan? Loan { get; set; }

        public DateOnly PaymentDate { get; set; }

        [Required]
        public byte Status { get; set; }  // 0=Pending, 1=Completed, 2=Failed

        public int? PayMethodId { get; set; }
        [ForeignKey("PayMethodId")]
        public virtual PaymentMethod? PaymentMethod { get; set; }

        public int? AccountId { get; set; }  // Account used for payment
        [ForeignKey("AccountId")]
        public virtual AccountBalance? AccountBalance { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; } // Amount deducted from account
    }
}
