using FinancialTransfers.Application.Interfaces.IUnitOfWork;
using FinancialTransfers.Infrastructure.Implementation.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransfers.Infrastructure.Implementation.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	public UnitOfWork(ApplicationDbContext context)
	{
		_context = context;
		Transfers = new TransferRepository(_context);
		Accounts = new AccountRepository(_context);
	}
	public ITransferRepository Transfers { get; private set; }
	public IAccountRepository Accounts { get; private set; }


	public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
