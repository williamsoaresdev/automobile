namespace AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles
{
    public interface IMongoRepositoryBase<T> where T : IDocument
    {
        Task InsertAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
}
