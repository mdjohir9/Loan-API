using Loan_API.DTO;
using Loan_API.Entities;
using Loan_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loan_API.Implementation
{
    public class RechargeRepository : GenericRepository<Recharge>, IRechargeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RechargeRepository(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<List<RechargeDetailDTO>> GetAllRechargeDetailsAsync()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from rc in _dbContext.Recharge
                        join cei in _dbContext.CustommerPersonnelInfo on rc.CustommerID equals cei.CustomerID into ceiGroup
            from cei in ceiGroup.DefaultIfEmpty()
                        join rca in _dbContext.RechargeAccount on rc.BankId equals rca.Id into rcaGroup
            from rca in rcaGroup.DefaultIfEmpty()
                        join rpm in _dbContext.RechargePaymentMethod on rc.PaymentMethodID equals rpm.Id into rpmGroup
                        from rpm in rpmGroup.DefaultIfEmpty()
                        join appuser in _dbContext.Users on rc.ApproveBy equals appuser.UserId into userGroup
                        from appuser in userGroup.DefaultIfEmpty()
                        join rejuser in _dbContext.Users on rc.RejectBy equals rejuser.UserId into RejuserGroup
                        from rejuser in RejuserGroup.DefaultIfEmpty()
                        join upuser in _dbContext.Users on rc.UpdatedBy equals upuser.UserId into UpuserGroup
                        from upuser in UpuserGroup.DefaultIfEmpty()
                        join applyuser in _dbContext.Users on rc.ApplyedBy equals applyuser.UserId into applyuserGroup
                        from applyuser in applyuserGroup.DefaultIfEmpty()
                        orderby rc.RechargeID descending
                        select new RechargeDetailDTO
                        {
                            RechargeID = rc.RechargeID,
                            BankAccountNumber = rc.BankAccountNumber,
                            Amount = rc.Amount,
                            RequestedDate = rc.RequestedDate,
                            IsApproved = rc.IsApproved,
                            BankTransactCode = rc.BankTransactCode,
                            AdminRemarks = rc.AdminRemarks,
                            CustommerID = rc.CustommerID,
                            FullName = cei.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cei.CustommerImage}",
                            CustCardNo = cei.CustCardNo,
                            BankOrWalletName = rca.BankOrWalletName,
                            AccountName = rca.AccountName,
                            AccountNumber = rca.AccountNumber,
                            PaymentMethodType = rpm.Name,
                            ApproveAt = rc.ApproveAt,
                            ApproveBy = appuser.Email,
                            RejectBy=rejuser.Email,
                            RejectedAt=rc.RejectAt,
                            UpdateBy=upuser.Email,
                            UpdatedAt=rc.UpdatedAt,
                            ApplyedBy=applyuser.Email,
                            ApplyedAt=rc.ApplyedAt,


                        };

            return await query.ToListAsync();
        }

        public async Task<List<RechargeDetailDTO>> GetlRechargeDetailsByeIdAsync(int customerId)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

            var query = from rc in _dbContext.Recharge
                        join cei in _dbContext.CustommerPersonnelInfo on rc.CustommerID equals cei.CustomerID into ceiGroup
                        from cei in ceiGroup.DefaultIfEmpty()
                        join rca in _dbContext.RechargeAccount on rc.BankId equals rca.Id into rcaGroup
                        from rca in rcaGroup.DefaultIfEmpty()
                        join rpm in _dbContext.RechargePaymentMethod on rc.PaymentMethodID equals rpm.Id into rpmGroup
                        from rpm in rpmGroup.DefaultIfEmpty()
                        join user in _dbContext.Users on rc.ApproveBy equals user.UserId into userGroup
                        from user in userGroup.DefaultIfEmpty()
                        join rejuser in _dbContext.Users on rc.RejectBy equals rejuser.UserId into RejuserGroup
                        from rejuser in RejuserGroup.DefaultIfEmpty()
                        join upuser in _dbContext.Users on rc.UpdatedBy equals upuser.UserId into UpuserGroup
                        from upuser in UpuserGroup.DefaultIfEmpty()
                        where rc.CustommerID == customerId
                        select new RechargeDetailDTO
                        {
                            RechargeID = rc.RechargeID,
                            BankAccountNumber = rc.BankAccountNumber,
                            Amount = rc.Amount,
                            RequestedDate = rc.RequestedDate,
                            IsApproved = rc.IsApproved,
                            BankTransactCode = rc.BankTransactCode,
                            AdminRemarks = rc.AdminRemarks,
                            CustommerID = rc.CustommerID,
                            FullName = cei.FullName,
                            CustommerImage = $"{baseUrl}/1111/CustommerImage/{cei.CustommerImage}",
                            CustCardNo = cei.CustCardNo,
                            BankOrWalletName = rca.BankOrWalletName,
                            AccountName = rca.AccountName,
                            AccountNumber = rca.AccountNumber,
                            PaymentMethodType = rpm.Name,
                            ApproveAt = rc.ApproveAt,
                            ApproveBy = user.Email,
                            RejectBy = rejuser.Email,
                            RejectedAt = rc.RejectAt,
                            UpdateBy = upuser.Email,
                            UpdatedAt = rc.UpdatedAt,
                        };

            return await query.ToListAsync();
        }

    }
}
