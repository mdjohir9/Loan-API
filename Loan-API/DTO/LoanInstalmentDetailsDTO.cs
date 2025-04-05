namespace Loan_API.DTO
{
    public class LoanInstalmentDetailsDTO
    {
        public int InstalmentID { get; set; }
        public int LoanID { get; set; }
        public DateOnly PaymentDate { get; set; }
        public byte? Status { get; set; }
        public decimal? AmountPaid { get; set; }
        public string? PayMethodName { get; set; }
        public string? LoanNumber { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime? LoanStartDate { get; set; }
        public DateTime? LoanEndDate { get; set; }
        public string? CustommerImage { get; set; }
        public string? FullName { get; set; }
        public string? CustCardNo { get; set; }
    }
}
