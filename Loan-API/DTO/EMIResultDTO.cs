namespace Loan_API.DTO
{
    public class EMIResultDTO
    {
        public decimal MonthlyInstallment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal LateCharge { get; set; }

    }
}
