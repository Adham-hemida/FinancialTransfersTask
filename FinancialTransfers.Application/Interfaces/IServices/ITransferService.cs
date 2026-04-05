using FinancialTransfers.Application.Contracts.Common;
using FinancialTransfers.Application.Contracts.Summary;
using FinancialTransfers.Application.Contracts.Transfer;

namespace FinancialTransfers.Application.Interfaces.IServices;
public interface ITransferService
{
	Task<Result<TransferResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
	Task<Result<PaginatedList<TransferResponse>>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken);
	Task<Result<TransferResponse>> AddAsync(TransferRequest request, CancellationToken cancellationToken = default);
	Task<Result> ToggleAsCompletedAsync(int id, CancellationToken cancellationToken);
	Task<Result> ToggleAsCancelledAsync(int id, CancellationToken cancellationToken);
	Task<Result<SummaryResponse>> GetSummaryAsync(CancellationToken cancellationToken = default);
}
