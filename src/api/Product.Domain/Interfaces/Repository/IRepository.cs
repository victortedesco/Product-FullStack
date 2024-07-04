namespace Product.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);

        Task<bool> Add(T model);
        Task<bool> Update(Guid id, T model);
        Task<bool> Delete(Guid id);
    }
}
