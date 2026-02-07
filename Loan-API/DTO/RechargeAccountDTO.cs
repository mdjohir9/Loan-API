using Loan_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class RechargeAccountDTO
    {
        [Required]
        public int RecPaymentMethodId { get; set; }

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
