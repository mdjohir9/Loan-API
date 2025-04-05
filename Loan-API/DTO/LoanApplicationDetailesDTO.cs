namespace Loan_API.DTO
{
    public class LoanApplicationDetailesDTO
    {
        public int CustomerID { get; set; }
        public string? CustCardNo { get; set; }
        public string? FullName { get; set; }
        public string? CustommerImage { get; set; }
        public string? Gender { get; set; }
        public decimal? LoanAmount { get; set; }
        public int? RepaymentPeriod { get; set; }
        public decimal? MonthlyInstallments { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string? PurposeOfLoan { get; set; }
        public byte? Status { get; set; }
        public string? PaymentMethodName { get; set; }
        public string? PlanName { get; set; }
    }
}
