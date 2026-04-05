namespace FinancialTransfers.Application.Contracts.Transfer;
public record TransferRequest
	(int FromAccountId, int ToAccountId, decimal Amount, string Currency, string Status, string? Description, decimal Fees = 0);
