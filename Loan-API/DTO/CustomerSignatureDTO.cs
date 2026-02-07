namespace Loan_API.DTO
{
    public class CustomerSignatureDTO
    {
        public int CustomerId { get; set; }
        public int  userid {get; set;}
        public List<string>? CustommerSignature { get; set; } = new List<string>();

    }
}
