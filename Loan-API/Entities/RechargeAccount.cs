using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_API.Entities
{
    public class RechargeAccount
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int RecPaymentMethodId { get; set; }
        [ForeignKey(nameof(RecPaymentMethodId))]
        public RechargePaymentMethod? RechargePaymentMethod { get; set; }

        [Required]
        public string? BankOrWalletName { get; set; }
        [Required]
        public string? AccountName { get; set; }

        [Required]
        public string? AccountNumber { get; set; }
        [Required]
        public Boolean? IsActive { get; set; }


    }
}
