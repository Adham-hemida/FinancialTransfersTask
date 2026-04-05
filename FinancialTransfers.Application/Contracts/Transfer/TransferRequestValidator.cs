using FluentValidation;

namespace FinancialTransfers.Application.Contracts.Transfer;
public class TransferRequestValidator : AbstractValidator<TransferRequest>
{
	public TransferRequestValidator()
	{
		RuleFor(x => x.ToAccountId)
			.NotEmpty()
			.WithMessage("Should be entre value")
			.GreaterThan(0)
			.WithMessage("ToAccountId should be greater than 0");

		RuleFor(x => x.FromAccountId)
			.NotEmpty()
			.WithMessage("Should be entre value")
			.GreaterThan(0)
			.WithMessage("FromAccountId should be greater than 0");

		RuleFor(x => x.Amount)
			.NotEmpty()
			.WithMessage("Amount should not be empty")
			.GreaterThan(0)
			.WithMessage("Amount must be greater than 0");

		RuleFor(x => x.Fees)
			.GreaterThanOrEqualTo(0)
			.WithMessage("Fees should not be negative");

		RuleFor(x => x.Status)
			.NotEmpty()
			.WithMessage("Status must be found")
			.Must(BePendingOrCancelledOrCompleted)
			.WithMessage("Status should be Pending or Completed or Cancelled");

		RuleFor(x => x.Description)
			.MaximumLength(1500)
			.WithMessage("Description must be lower than 1500");

		RuleFor(x => x.Currency)
			.NotEmpty()
			.WithMessage("Currency is required.")
			.Length(1, 10)
			.WithMessage("Currency should be between 1 to 10");

	}

	private bool BePendingOrCancelledOrCompleted(string status)
	{
		if (status == "Pending" || status == "Completed" || status == "Cancelled")
			return true;

		return false;
	}
}
