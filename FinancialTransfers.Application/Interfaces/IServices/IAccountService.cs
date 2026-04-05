using FinancialTransfers.Application.Contracts.Account;
using FinancialTransfers.Application.Contracts.Common;

namespace FinancialTransfers.Application.Interfaces.IServices;
public interface IAccountService
{
	Task<Result<AccountResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
	Task<Result<PaginatedList<AccountResponse>>> GetAllAsync(RequestFilters filters, CancellationToken cancellationToken);
	Task<Result<AccountResponse>> AddAsync(AccountRequest request, CancellationToken cancellationToken = default);
	}
