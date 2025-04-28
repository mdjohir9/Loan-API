using System.Security.Cryptography.X509Certificates;

namespace Loan_API.DTO
{
    public class LoanStatementDTO
    {
        public int LoanId { get; set; }
        public int CustomerID { get; set; }
        public string? CustCardNo { get; set; }
        public string? FullName { get; set; }
        public string? CustommerImage { get; set; }
        public string? Gender { get; set; }
        public decimal? LoanAmount { get; set; }
        public int? RepaymentPeriod { get; set; }
        public decimal? MonthlyInstallments { get; set; }
        public DateTime? DisbursementDate { get; set; }
        public string? PurposeOfLoan { get; set; }
        public byte? Status { get; set; }
        public string? PaymentMethodName { get; set; }
        public string? PlanName { get; set; }
        public string? CompanyLogo { get; set; }
        public string? CompanyName { get; set; }
        public string? BankLogo { get; set; }
        public string? AuthorizeSignature { get; set; }
        public string? Approvelogo { get; set; }
        
    }
}
