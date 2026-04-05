namespace FinancialTransfers.Domain.Entites;

public class Transfer
{
	public int Id { get; set; }

	public decimal Amount { get; set; }

	public decimal Fees { get; set; } = 0;

	public string Currency { get; set; } =string.Empty;

	public string Status { get; set; }=TransferStatus.Pending;

	public string? Description { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public decimal NetAmount => Amount - Fees;

	public int FromAccountId { get; set; }
	public int ToAccountId { get; set; }

	public Account FromAccount { get; set; } = default!;
	public Account ToAccount { get; set; } = default!;
}
