using FinancialTransfers.Application.Contracts.Summary;

namespace FinancialTransfers.Application.Interfaces.IRepository;
public interface ITransferRepository: IGenericRepository<Transfer>
{
	Task<SummaryResponse> GetSummary(CancellationToken cancellationToken = default);
}
