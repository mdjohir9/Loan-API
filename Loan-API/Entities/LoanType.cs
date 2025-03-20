using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class LoanType
    {
        [Key]
        public int LoanTypeID { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LoanTypeName { get; set; }  // e.g., Car Loan, Business Loan

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
