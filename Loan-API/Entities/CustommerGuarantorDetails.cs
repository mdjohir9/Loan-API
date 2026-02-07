using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_API.Entities
{
    public class CustommerGuarantorDetails
    {
        [Key]
        public int ID { get; set; }

        public string? GuarantorImage { get; set; } // Stores the image as binary data

        public string? GuarantorFullName { get; set; }

        public string? RelationshipWithApplicant { get; set; }

        public string? GuarantorContactNumber { get; set; }

        public string? GuarantorAddress { get; set; }

        public string? GuarantorNationalIDOrPassport { get; set; }

        public string? GuarantorSignature { get; set; } // Stores the signature as binary data
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual CustommerPersonnelInfo? CustommerPersonnelInfo { get; set; }
    }
}
