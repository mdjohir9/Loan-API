using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class RechargePaymentMethodDTO
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
