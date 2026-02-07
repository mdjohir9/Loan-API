namespace Loan_API.DTO
{
    public class LoanLimitDTO
    {
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public int MinRepaymentPeriod { get; set; }
        public int MaxRepaymentPeriod { get; set; }

    }
}
