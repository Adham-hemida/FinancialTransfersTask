namespace FinancialTransfers.Application.Contracts.Transfer;
public record TransferResponse (int Id, int FromAccountId, int ToAccountId,  string Currency, string Status, string? Description, decimal Amount, decimal NetAmount,decimal Fees = 0);
