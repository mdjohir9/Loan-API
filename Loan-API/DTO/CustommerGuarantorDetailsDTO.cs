namespace Loan_API.DTO
{
    public class CustommerGuarantorDetailsDTO
    {
        public int CustomerID { get; set; }
        public string? GuarantorImage { get; set; }
        public string? GuarantorFullName { get; set; }
        public string? RelationshipWithApplicant { get; set; }
        public string? GuarantorContactNumber { get; set; }
        public string? GuarantorAddress { get; set; }
        public string? GuarantorNationalIDOrPassport { get; set; }
        public string? GuarantorSignature { get; set; }
    }
}
