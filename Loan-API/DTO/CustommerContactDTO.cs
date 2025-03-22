namespace Loan_API.DTO
{
    public class CustommerContactDTO
    {
        public int CustomerID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AlternativePhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? PreStreet { get; set; }
        public string? PerStreet { get; set; }
        public string? PreZIP { get; set; }
        public string? PerZIP { get; set; }
        public string? PreCity { get; set; }
        public string? PerCity { get; set; }
        public string? PreState { get; set; }
        public string? PerState { get; set; }
    }
}
