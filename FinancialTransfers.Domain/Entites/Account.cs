namespace FinancialTransfers.Domain.Entites;
public class Account
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Type { get; set; }=string.Empty;

	public decimal Balance { get; set; }

	public string Currency { get; set; } =string.Empty;

	public bool IsActive { get; set; } = true;

	public ICollection<Transfer> ToTransfers { get; set; } = [];
	public ICollection<Transfer> FromTransfers { get; set; } = [];
}
