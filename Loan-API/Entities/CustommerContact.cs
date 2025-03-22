using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_API.Entities
{
    public class CustommerContact
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(20)]
        public string? AlternativePhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        public string? PreStreet { get; set; }


        public string? PerStreet { get; set; }

        public string? PreZIP { get; set; }


        public string? PerZIP { get; set; }

   
        public string? PreCity { get; set; }

        public string? PerCity { get; set; }


        public string? PreState { get; set; }


        public string? PerState { get; set; }


    
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual CustommerPersonnelInfo? CustommerPersonnelInfo { get; set; }
    }
}
