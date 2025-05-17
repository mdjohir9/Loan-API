using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class WithdrawRepository : GenericRepository<Withdraw>, IWithdrawRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WithdrawRepository(ApplicationDbContext dbContext , IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<WithdrawDetailDTO>> GetAllWithdrawDetailsAsync()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from wd in _dbContext.Withdraw
                        join cei in _dbContext.CustommerPersonnelInfo on wd.CustommerID equals cei.CustomerID into ceiGroup
                        from cei in ceiGroup.DefaultIfEmpty()
                        join rpm in _dbContext.RechargePaymentMethod on wd.PaymentMethodID equals rpm.Id into rpmGroup
                        from rpm in rpmGroup.DefaultIfEmpty()
                        join rca in _dbContext.RechargeAccount on wd.BankId equals rca.Id into rcaGroup
                        from rca in rcaGroup.DefaultIfEmpty()
                        orderby wd.WithdrawaID descending
                        select new WithdrawDetailDTO
                        {
                            WithdrawaID = wd.WithdrawaID,
                            BankName = rca.BankOrWalletName,
                            AccountNumber = wd.AccountNumber,
                            Amount = wd.Amount,
                            RequestedDate = wd.RequestedDate,
                            IsApproved = wd.IsApproved,
                            TransactionCode = wd.TransactionCode,
                            AdminRemarks = wd.AdminRemarks,
                            ApproveAt = wd.ApproveAt,
                            ApproveBy = wd.ApproveBy,
                            CustommerID = wd.CustommerID,
                            FullName = cei.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cei.CustommerImage}",
                            CustCardNo = cei.CustCardNo,
                            PaymentMethodType = rpm.Name
                        };

            return await query.ToListAsync();
        }
        public async Task<List<WithdrawDetailDTO>> GetWithdrawDetailsByCustomerIdAsync(int customerId)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from wd in _dbContext.Withdraw
                        join cei in _dbContext.CustommerPersonnelInfo on wd.CustommerID equals cei.CustomerID into ceiGroup
                        from cei in ceiGroup.DefaultIfEmpty()   
                        join rpm in _dbContext.RechargePaymentMethod on wd.PaymentMethodID equals rpm.Id into rpmGroup
                        from rpm in rpmGroup.DefaultIfEmpty()
                        join rca in _dbContext.RechargeAccount on wd.BankId equals rca.Id into rcaGroup
                        from rca in rcaGroup.DefaultIfEmpty()
                        where wd.CustommerID == customerId
                        orderby wd.WithdrawaID descending
                        select new WithdrawDetailDTO
                        {
                            WithdrawaID = wd.WithdrawaID,
                            BankName = rca.BankOrWalletName,
                            AccountNumber = wd.AccountNumber,
                            Amount = wd.Amount,
                            RequestedDate = wd.RequestedDate,
                            IsApproved = wd.IsApproved,
                            TransactionCode = wd.TransactionCode,
                            AdminRemarks = wd.AdminRemarks,
                            ApproveAt = wd.ApproveAt,
                            ApproveBy = wd.ApproveBy,
                            CustommerID = wd.CustommerID,
                            FullName = cei.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cei.CustommerImage}",
                            CustCardNo = cei.CustCardNo,
                            PaymentMethodType = rpm.Name
                        };

            return await query.ToListAsync();
        }


    }
}
