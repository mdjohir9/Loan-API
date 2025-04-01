using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class Loan
    {
        [Key]
        public int LoanID { get; set; }

        [Required]
        public string LoanNumber { get; set; } = null!; // Unique loan identifier


        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; } // Total loan amount
       
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; } // Total loan amount
    
        [Column(TypeName = "decimal(18,2)")]
        public decimal DueAmount { get; set; } // Total loan amount

        [Required]
        public int TenureMonths { get; set; } // Loan period in months



        [Required]
        public DateTime LoanStartDate { get; set; } = DateTime.UtcNow;

        public DateTime? LoanEndDate { get; set; } // Nullable, updated when loan is closed

        [Required]
        public byte LoanStatus { get; set; } //0=pending 1=Active 2=Closed

        public string? Purpose { get; set; } // Purpose of the loan (optional)

        // Foreign key to Payment Method (optional if a default payment method is set)

        public DateTime? DisbursementDate { get; set; }

        [Required]
        public int CustomerID { get; set; } // Foreign Key to Customer

        [ForeignKey(nameof(CustomerID))]
        public CustommerPersonnelInfo Customer { get; set; } = null!;


        public int? PayMethodId { get; set; }

        [ForeignKey(nameof(PayMethodId))]
        public PaymentMethod? PaymentMethod { get; set; }

        // Relationship with Transactions
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
