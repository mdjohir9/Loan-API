using Loan_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class RechargeRequestDTO
    {
        public string? BankAccountNumber { get; set; }

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

        public List<string>? Statement { get; set; } = new List<string>();

        [Required]
        public int PaymentMethodID { get; set; }//this is Owaner Part
        [Required]
        public int BankId { get; set; } // this is a Owaner Bank Account Number Whwere Brower menualy Transfer Money


        [Required]
        public int CustommerID { get; set; }
        public int? UserId { get; set; }
        


    }
}
