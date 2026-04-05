using FinancialTransfers.Application.Contracts.Common;
using Mapster;
using System.Linq.Dynamic.Core;
namespace FinancialTransfers.Infrastructure.Implementation.Services;
public class AccountSevice(IUnitOfWork unitOfWork) : IAccountService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<AccountResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
	{
		var account = await _unitOfWork.Accounts.GetByIdAsync(id, cancellationToken);

		if (account is null)
			return Result.Failure<AccountResponse>(AccountError.AccountNotFound);

		return Result.Success(account.Adapt<AccountResponse>());
	}



	public async Task<Result<PaginatedList<AccountResponse>>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken)
	{
		var query = _unitOfWork.Accounts.GetAsQueryable().Where(x => x.IsActive);

		if (!string.IsNullOrEmpty(filters.SearchValue))
		{
			query = query.Where(x => x.Name.Contains(filters.SearchValue));
		}

		if (!string.IsNullOrEmpty(filters.SortColumn))
		{
			query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
		}

		var source = query.AsNoTracking()
			.ProjectToType<AccountResponse>();

		var account = await PaginatedList<AccountResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

		return Result.Success(account);
	}



	public async Task<Result<AccountResponse>> AddAsync(AccountRequest request, CancellationToken cancellationToken = default)
	{
		var existingAccount = await _unitOfWork.Accounts.GetAsQueryable()
			  .AnyAsync(x => x.Name == request.Name, cancellationToken);

		if (existingAccount)
			return Result.Failure<AccountResponse>(AccountError.AccountDuplicated);

		var account = request.Adapt<Account>();

		await _unitOfWork.Accounts.AddAsync(account, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		return Result.Success(account.Adapt<AccountResponse>());
	}
}
