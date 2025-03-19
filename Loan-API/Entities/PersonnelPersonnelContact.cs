using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class PersonnelPersonnelContact
    {
        [Key]
        public int ContactID { get; set; }

        [Required]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public string? AlternativePhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        public string? CurrentAddress { get; set; } // Street, City, State, ZIP

        public string? PermanentAddress { get; set; } // If different from current
    }
}
