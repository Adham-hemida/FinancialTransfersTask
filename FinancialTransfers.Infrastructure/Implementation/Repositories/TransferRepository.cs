using FinancialTransfers.Application.Contracts.Summary;
using FinancialTransfers.Domain.Consts;

namespace FinancialTransfers.Infrastructure.Implementation.Repositories;
public class TransferRepository : GenericRepository<Transfer>, ITransferRepository
{
	private readonly ApplicationDbContext _context;
	public TransferRepository(ApplicationDbContext context) : base(context)
	{
		_context = context;
	}

	public async Task<SummaryResponse> GetSummary(CancellationToken cancellationToken=default)
	{
		var totalCount=await _context.Transfers.CountAsync(cancellationToken);
		var completed = await _context.Transfers.CountAsync(x => x.Status == TransferStatus.Completed, cancellationToken);
		var pending=await _context.Transfers.CountAsync(x=>x.Status == TransferStatus.Pending, cancellationToken);
		var totalNetAmount=await _context.Transfers.Where(t=>t.Status== TransferStatus.Completed).SumAsync(x=>x.Amount);

		var response=new SummaryResponse(totalCount, completed, pending, totalNetAmount);
		return response;
	}
}

