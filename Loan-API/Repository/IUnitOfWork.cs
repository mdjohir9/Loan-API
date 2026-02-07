using Microsoft.EntityFrameworkCore.Storage;

namespace Loan_API.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        ICustommerRepository Custommer { get; }
        ICustommerContactRepository Contact { get; }
        ICustommerFinancialInfoRepository FinancialInfo { get; }
        ICustommerEmploymentRepository Employment { get; }
        ICustommerGuarantorRepository Guarantor { get; }
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }
        ILoginRepository Login { get; }
        ILoanApplicationRepository LoanApplication { get; }
        IloanRepository Loan { get; }
        ILoanInstalmentRepository LoanInstalment { get; }
        ILoanPlanRepository LoanPlan { get; }
        ITransctionRepository Transction { get; }
        IRechargeAccountRepository RechargeAccount { get; }
        IRechargePaymentMethodRepository RechargePaymentMethod { get; }

        IAccountRepository Account { get; }
        IRechargeRepository Recharge { get; }
        IWithdrawRepository Withdraw { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> Save();
    }
}
