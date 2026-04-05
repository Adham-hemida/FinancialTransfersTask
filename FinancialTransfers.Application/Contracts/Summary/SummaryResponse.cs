namespace FinancialTransfers.Application.Contracts.Summary;
public record SummaryResponse(int TotalCount, int Completed,int Pending, decimal TotalAmount);
