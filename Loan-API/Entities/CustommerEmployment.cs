using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_API.Entities
{
    public class CustommerEmployment
    {
        [Key]
        public int ID { get; set; }


        public string? EmploymentType { get; set; } // Salaried, Self-Employed, Business Owner

        [MaxLength(200)]
        public string?EmployerOrBusnName { get; set; }
        [MaxLength(100)]
        public string? JobTitleOrBusnType { get; set; }

        public decimal? MonthlyIncOrBusnRev { get; set; }

        public int? YearsOfExpOrBusnAge { get; set; }

        public string? WorkOrBusnAddress { get; set; }

        public string? EmployerOrBusnContact { get; set; }

      
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual CustommerPersonnelInfo? CustommerPersonnelInfo { get; set; }
    }
}
