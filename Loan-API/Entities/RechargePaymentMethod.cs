using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class RechargePaymentMethod
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public Boolean? IsActive { get; set; } 
        public ICollection<RechargeAccount> RechargeAccounts { get; set; } = new List<RechargeAccount>();
    }
}
