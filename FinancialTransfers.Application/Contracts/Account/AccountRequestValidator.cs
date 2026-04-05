using FluentValidation;

namespace FinancialTransfers.Application.Contracts.Account;
public class AccountRequestValidator : AbstractValidator<AccountRequest>
{
	public AccountRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Name is required.")
			.Length(3,250)
			.WithMessage("Name must be lower than 250");

		RuleFor(x => x.Balance)
			.GreaterThanOrEqualTo(0)
			.WithMessage("Balance must be greater than or equal to 0.");

		RuleFor(x => x.Currency)
			.NotEmpty()
			.WithMessage("Currency is required.")
			.Length(1,10)
			.WithMessage("Currency should be between 1 to 10");
		
		RuleFor(x => x.Type)
			.NotEmpty()
			.WithMessage("Type is required.")
			.Must(BeEqualBankOrTreasury)
			.WithMessage("Type must be either 'Bank' or 'Treasury'.");

	}
	private bool BeEqualBankOrTreasury(string Type)
	{
		if(Type=="Treasury" || Type== "Bank")
			return true;

		return false;
	}
}
