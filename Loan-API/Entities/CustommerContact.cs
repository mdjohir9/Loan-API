using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_API.Entities
{
    public class CustommerContact
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(20)]
        public string? AlternativePhoneNumber { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "EmailAddress is required")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "PreStreet is required")]
        public string? PreStreet { get; set; }


        [Required(ErrorMessage = "PreStreet is required")]
        public string? PerStreet { get; set; }

        [Required(ErrorMessage = "PreZIP is required")]
        public string? PreZIP { get; set; }

        [Required(ErrorMessage = "PerZIP is required")]
        public string? PerZIP { get; set; }

        [Required(ErrorMessage = "PreCity is required")]
        public int PreCity { get; set; }

        [Required(ErrorMessage = "PerCity is required")]
        public int PerCity { get; set; }

        [Required(ErrorMessage = "PreState is required")]
        public int PreState { get; set; }

        [Required(ErrorMessage = "PerState is required")]
        public int PerState { get; set; }


        [Required(ErrorMessage = "CustomerID is required")]
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual CustommerPersonnelInfo? CustommerPersonnelInfo { get; set; }
    }
}
