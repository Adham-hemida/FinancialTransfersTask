namespace FinancialTransfers.Application.Contracts.Account;
public record AccountResponse(int Id,string Name,string Type, decimal Balance, string Currency,bool IsActive);
