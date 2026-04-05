using FinancialTransfers.Application.Interfaces.IRepository;
using FinancialTransfers.Infrastructure.Data;

namespace FinancialTransfers.Infrastructure.Implementation.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
	private readonly ApplicationDbContext _context;
	public GenericRepository(ApplicationDbContext context)
	{
		_context = context;

	}	
	
	public IQueryable<T> GetAsQueryable()
	{
		return _context.Set<T>().AsQueryable();
	}

	public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
	{
		await _context.Set<T>().AddAsync(entity);
	}

	public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
	{
		await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
	}

	public void Delete(T entity, CancellationToken cancellationToken = default)
	{
		_context.Set<T>().Remove(entity);
	}

	public void DeleteRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
	{
		_context.Set<T>().RemoveRange(entities);
	}


	public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
	}


	public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await _context.Set<T>().FindAsync(id, cancellationToken);
	}

	public void Update(T entity)
	{
		_context.Set<T>().Update(entity);
	}
}
