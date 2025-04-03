namespace Loan_API.DTO
{
    public class EMIResultDTO
    {
        public decimal MonthlyInstallment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalPayable { get; set; }
    }
}
