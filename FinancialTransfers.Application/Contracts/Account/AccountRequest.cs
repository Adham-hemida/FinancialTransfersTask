namespace FinancialTransfers.Application.Contracts.Account;

public record AccountRequest(string Name, decimal Balance, string Currency, string Type);
