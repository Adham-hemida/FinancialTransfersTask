using FinancialTransfers.Application.Contracts.Common;
using FinancialTransfers.Application.Contracts.Summary;
using FinancialTransfers.Application.Contracts.Transfer;
using FinancialTransfers.Domain.Consts;
using Mapster;
using System.Linq.Dynamic.Core;

namespace FinancialTransfers.Infrastructure.Implementation.Services;
public class TransferService(IUnitOfWork unitOfWork, ITransferRepository transferRepository) : ITransferService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly ITransferRepository _transferRepository = transferRepository;

	public async Task<Result<TransferResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
	{
		var transfer = await _unitOfWork.Transfers.GetByIdAsync(id, cancellationToken);

		if (transfer is null)
			return Result.Failure<TransferResponse>(TransferError.TransferNotFound);

		return Result.Success(transfer.Adapt<TransferResponse>());
	}



	public async Task<Result<PaginatedList<TransferResponse>>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Transfers.GetAsQueryable();

		if (!string.IsNullOrEmpty(filters.SearchValue))
		{
			query = query.Where(x => x.Status.Contains(filters.SearchValue));
		}

		if (!string.IsNullOrEmpty(filters.SortColumn))
		{
			query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
		}

		var source = query.AsNoTracking()
			.ProjectToType<TransferResponse>();

		var transfer = await PaginatedList<TransferResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

		return Result.Success(transfer);
	}



	public async Task<Result<TransferResponse>> AddAsync(TransferRequest request, CancellationToken cancellationToken = default)
	{
		var toAccount = await _unitOfWork.Accounts.GetByIdAsync(request.ToAccountId, cancellationToken);

		if (toAccount is null)
			return Result.Failure<TransferResponse>(AccountError.AccountNotFound);

		var fromAccount = await _unitOfWork.Accounts.GetByIdAsync(request.FromAccountId, cancellationToken);

		if (fromAccount is null)
			return Result.Failure<TransferResponse>(AccountError.AccountNotFound);

		if (request.ToAccountId == request.FromAccountId)
			return Result.Failure<TransferResponse>(TransferError.SameAccount);

		if (toAccount.Currency != fromAccount.Currency)
			return Result.Failure<TransferResponse>(TransferError.CurrencyNotMatch);

		if (request.Amount > fromAccount.Balance)
			return Result.Failure<TransferResponse>(TransferError.InsufficientBalance);

		if (request.Fees > request.Amount)
			return Result.Failure<TransferResponse>(TransferError.InvalidFees);

		fromAccount.Balance -= request.Amount - request.Fees;
		toAccount.Balance += request.Amount;
		
		var transfer = request.Adapt<Transfer>();

		await _unitOfWork.Transfers.AddAsync(transfer, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(transfer.Adapt<TransferResponse>());
	}


	public async Task<Result> ToggleAsCompletedAsync(int id, CancellationToken cancellationToken)
	{
		var transfer = await _unitOfWork.Transfers.GetByIdAsync(id, cancellationToken);

		if (transfer is null)
			return Result.Failure(TransferError.TransferNotFound);

		if (transfer.Status != TransferStatus.Pending)
			return Result.Failure(TransferError.CannotCompletedTransfer);

		transfer.Status = TransferStatus.Completed;

		_unitOfWork.Transfers.Update(transfer);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}


	public async Task<Result> ToggleAsCancelledAsync(int id, CancellationToken cancellationToken)
	{
		var transfer = await _unitOfWork.Transfers.GetByIdAsync(id, cancellationToken);

		if (transfer is null)
			return Result.Failure(TransferError.TransferNotFound);

		if (transfer.Status != TransferStatus.Pending)
			return Result.Failure(TransferError.CannotCancelTransfer);

		transfer.Status = TransferStatus.Cancelled;

		_unitOfWork.Transfers.Update(transfer);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success();
	}


	public async Task<Result<SummaryResponse>> GetSummaryAsync( CancellationToken cancellationToken = default)
	{
		var response = await _transferRepository.GetSummary(cancellationToken);
		return Result.Success(response);

	}

}
