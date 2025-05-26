using Loan_API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.DTO
{
    public class LoanApplicationDTO
    {
   


        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public int RepaymentPeriod { get; set; } // In months

        [Required]
        [MaxLength(255)]
        public string? PurposeOfLoan { get; set; }



        //[Required]
        //public byte Status { get; set; } //0= unpaid 1=paid  

        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

 


        [Required]
        public int PlanID { get; set; }
        [Required]

        public int CustomerID { get; set; }
        [Required]
        public int PayMethodID { get; set; }
        public int? UserId { get; set; }
        public int? ApplicationId { get; set; }


    }
}
