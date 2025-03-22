namespace Loan_API.Repository
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByCompanyIdAsync(string CompanyId);
       // Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<IEnumerable<T>> GetAllWithOutDeletedAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(string id);


        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, string propertyName, object value);

        Task SoftDeleteAsync(int id, int deletedBy);
        Task DeleteAsync(int id);
        Task<string> SaveDocumentsListsAsync(List<string> documentsBase64, string id, string companyId, string documentType);

    }
}
