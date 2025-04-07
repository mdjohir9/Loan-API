namespace Loan_API.DTO
{
    public class CustommerFinancialInfoDTO
    {
        public int CustomerID { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal MonthlyIncomeSources { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public string? AssetsOwned { get; set; }
        public string? Liabilities { get; set; }
        public int? UserId { get; set; }
    }
}
