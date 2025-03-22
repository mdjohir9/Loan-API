using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loan_API.Entities
{
    public class CustommerFinancialInfo
    {
        [Key]
        public int ID { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal MonthlyIncomeSources { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public string? AssetsOwned { get; set; } // Land, House, Vehicles, Business Equipment

        public string? Liabilities { get; set; } // Other Loans, Credit Card Debts
        public int? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual CustommerPersonnelInfo? CustommerPersonnelInfo { get; set; }
    }
}
