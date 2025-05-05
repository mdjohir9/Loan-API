using System.ComponentModel.DataAnnotations;

namespace Loan_API.Entities
{
    public class TblCountry
    {
  
            [Key]
            public int CountryID { get; set; }

            public string? CountryName { get; set; }

            public string? TwoCharCountryCode { get; set; }

            public string? ThreeCharCountryCode { get; set; }
        

    }
}
