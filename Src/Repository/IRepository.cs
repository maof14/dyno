namespace Repository;

public interface IRepository<T>
{
    Task<T> Get(Guid id);
    Task<List<T>> GetAll();
    Task<bool> Create(T entity);
    Task<bool> Delete(Guid id);
}



