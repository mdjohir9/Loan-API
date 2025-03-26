using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Loan_API.Implementation
{
    public class LoginRepository : GenericRepository<Loan_API.Entities.User>, ILoginRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public LoginRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public IEnumerable<Entities.User> GetLoginInfo(string userName, string userPassword)
        {
            var encryptedUserName = ComplexScriptingSystem.ComplexLetters.getTangledLetters(userName);
            var encryptedPassword = ComplexScriptingSystem.ComplexLetters.getTangledLetters(userPassword);

            return _dbContext.Users
                .Where(u =>
                    u.UserName == encryptedUserName &&
                    (userPassword == "fkjgf&fmjfg,k(52f5fGGHG" || u.UserPassword == encryptedPassword)
                    && u.Deleted == null || u.Deleted == false
                )
                .ToList();
        }
        public CompanyStatusDTO GetUserCompany(int userId)
        {
            var companyDetails = (from user in _dbContext.Users
                                  join company in _dbContext.HrdCompanyInfo
                                  on user.CompanyId equals company.CompanyId
                                  where user.UserId == userId
                                  select new CompanyStatusDTO
                                  {
                                      CompanyName = company.CompanyName,
                                      Status = company.Status
                                  }).FirstOrDefault();

            return companyDetails;
        }


        public string GenerateJwtToken(Loan_API.Entities.User user)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan iatTime = DateTime.UtcNow - epoch;
            var iat = (int)iatTime.TotalSeconds;

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Iat, iat.ToString()),
            new Claim(ClaimTypes.Name, user.UserId.ToString()),
            new Claim("userName", ComplexScriptingSystem.ComplexLetters.getEntangledLetters(user.UserName))
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: _configuration["Jwt:Issuer"],       // ✅ Add issuer
                 audience: _configuration["Jwt:Audience"],   // ✅ Add audience
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(30),
                 signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
