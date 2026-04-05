
namespace FinancialTransfers.Application.Interfaces.IUnitOfWork;
public interface IUnitOfWork : IDisposable
{

	IAccountRepository Accounts { get; }
	ITransferRepository Transfers { get; }
	Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
