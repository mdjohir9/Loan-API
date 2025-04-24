using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Loan
    {
        [Key]
        public int LoanID { get; set; }

        [Required]
        public string LoanNumber { get; set; } = null!; 


        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DepositAmount { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal? LateCharge { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; } 
    
        [Column(TypeName = "decimal(18,2)")]
        public decimal DueAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPayableAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalInterest { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MonthlyInstallment { get; set; }
        [Required]
        public int TenureMonths { get; set; } 



        [Required]
        public DateTime LoanStartDate { get; set; } = DateTime.UtcNow;

        public DateTime? LoanEndDate { get; set; } 

        [Required]
        public byte LoanStatus { get; set; }

        public string? Purpose { get; set; } 

     

        public DateTime? DisbursementDate { get; set; }

        [Required]
        public int CustomerID { get; set; } 

        [ForeignKey(nameof(CustomerID))]
        public CustommerPersonnelInfo Customer { get; set; } = null!;


        public int? PayMethodId { get; set; }

        [ForeignKey(nameof(PayMethodId))]
        public PaymentMethod? PaymentMethod { get; set; }

        public int? PlanID { get; set; }

        [ForeignKey(nameof(PlanID))]
        public LoanPlan? LoanPlan { get; set; }
    }
}
