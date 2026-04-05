namespace FinancialTransfers.Application.ErrorHandler;
public static class AccountError
{
	public static readonly Error AccountNotFound =
	new("Account.not_found", "No Account was found with the given Id", StatusCodes.Status404NotFound);

	public static readonly Error AccountDuplicated =
		new("Account.Duplicated", " Account  is already exist", statusCode: StatusCodes.Status409Conflict);

}
