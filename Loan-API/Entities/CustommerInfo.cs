using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class CustommerInfo
    {
        [Key]
        public int ID { get; set; }

        public string? CustommerImage { get; set; } // Stores the image as binary data

        [Required]
        [MaxLength(200)]
        public string? FullName { get; set; }

        [Required]
        public string? Gender { get; set; } // Male, Female, Other

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Nationality { get; set; }

        public string? MaritalStatus { get; set; } // Single, Married, Divorced

        public string? EducationLevel { get; set; } // High School, Bachelor, Master, PhD

        [MaxLength(100)]
        public string? Occupation { get; set; }
    }
}
