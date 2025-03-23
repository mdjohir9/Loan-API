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
        Task<int> Save();
    }
}
