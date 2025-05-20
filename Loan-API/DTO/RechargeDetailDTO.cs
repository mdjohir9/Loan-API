namespace Loan_API.DTO
{
    public class RechargeDetailDTO
    {
        public int RechargeID { get; set; }
        public string? BankAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool? IsApproved { get; set; }
        public string? BankTransactCode { get; set; }
        public string? AdminRemarks { get; set; }
        public int CustommerID { get; set; }
        public string? FullName { get; set; }
        public string? CustommerImage { get; set; }
        public string? CustCardNo { get; set; }
        public string? BankOrWalletName { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? PaymentMethodType { get; set; }
        public DateTime? ApproveAt { get; set; }
        public string? ApproveBy { get; set; }
        public string? RejectBy { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
