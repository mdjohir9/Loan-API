namespace Loan_API.DTO
{
    public class LoanBalanceDto
    {
        public decimal BalanceAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal MonthlyInstallment { get; set; }
    }
}
