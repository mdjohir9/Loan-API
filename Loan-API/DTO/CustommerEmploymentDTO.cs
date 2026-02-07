namespace Loan_API.DTO
{
    public class CustommerEmploymentDTO
    {
        public int CustomerID { get; set; }
        public string? EmploymentType { get; set; }
        public string? EmployerOrBusnName { get; set; }
        public string? JobTitleOrBusnType { get; set; }
        public decimal? MonthlyIncOrBusnRev { get; set; }
        public int? YearsOfExpOrBusnAge { get; set; }
        public string? WorkOrBusnAddress { get; set; }
        public string? EmployerOrBusnContact { get; set; }
    }
}
