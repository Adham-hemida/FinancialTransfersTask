namespace FinancialTransfers.Application.Interfaces.IRepository;
public interface IGenericRepository<T> where T : class
{
	IQueryable<T> GetAsQueryable();
	Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
	Task AddAsync(T entity, CancellationToken cancellationToken = default);
	void Update(T entity);
	void Delete(T entity, CancellationToken cancellationToken = default);
	void DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
	Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

}
