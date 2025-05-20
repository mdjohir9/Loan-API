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
                        join user in _dbContext.Users on wd.ApproveBy equals user.UserId into userGroup
                        from user in userGroup.DefaultIfEmpty()
                        join rejuser in _dbContext.Users on wd.RejectBy equals rejuser.UserId into RejuserGroup
                        from rejuser in RejuserGroup.DefaultIfEmpty()
                        join upuser in _dbContext.Users on wd.UpdatedBy equals upuser.UserId into UpuserGroup
                        from upuser in UpuserGroup.DefaultIfEmpty()
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
                            CustommerID = wd.CustommerID,
                            FullName = cei.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cei.CustommerImage}",
                            CustCardNo = cei.CustCardNo,
                            PaymentMethodType = rpm.Name,
                            ApproveAt = wd.ApproveAt,
                            ApproveBy = user.Email,
                            RejectBy = rejuser.Email,
                            RejectedAt = wd.RejectAt,
                            UpdateBy = upuser.Email,
                            UpdatedAt = wd.UpdatedAt,
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
                        join user in _dbContext.Users on wd.ApproveBy equals user.UserId into userGroup
                        from user in userGroup.DefaultIfEmpty()
                        join rejuser in _dbContext.Users on wd.RejectBy equals rejuser.UserId into RejuserGroup
                        from rejuser in RejuserGroup.DefaultIfEmpty()
                        join upuser in _dbContext.Users on wd.UpdatedBy equals upuser.UserId into UpuserGroup
                        from upuser in UpuserGroup.DefaultIfEmpty()
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
                            CustommerID = wd.CustommerID,
                            FullName = cei.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cei.CustommerImage}",
                            CustCardNo = cei.CustCardNo,
                            PaymentMethodType = rpm.Name,
                            ApproveAt = wd.ApproveAt,
                            ApproveBy = user.Email,
                            RejectBy = rejuser.Email,
                            RejectedAt = wd.RejectAt,
                            UpdateBy = upuser.Email,
                            UpdatedAt = wd.UpdatedAt,
                        };

            return await query.ToListAsync();
        }


    }
}
