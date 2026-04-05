namespace FinancialTransfers.Application.ErrorHandler;
public static class TransferError
{


	public static readonly Error TransferNotFound =
new("Transfer.not_found", "No Transfer was found with the given Id", StatusCodes.Status404NotFound);
	
	public static readonly Error SameAccount=
new("Transfer.Duplicated", "FromAccountId must be different with ToAccountId", StatusCodes.Status400BadRequest);

	public static readonly Error InvalidFees =
		new("Transfer.invalid_fees", "Fees cannot be greater than the transfer amount", StatusCodes.Status400BadRequest);

	public static readonly Error CurrencyNotMatch =
	new("Transfer.currency_NotMatch ", "Cannot transfer between accounts with different currencies", StatusCodes.Status400BadRequest);

	public static readonly Error InsufficientBalance =
	new("Transfer.insufficient_balance", "Source account balance is insufficient to complete the transfer", StatusCodes.Status400BadRequest);

	public static readonly Error TransferDuplicated =
		new("Transfer.Duplicated", " Transfer  is already exist", statusCode: StatusCodes.Status409Conflict);

	public static readonly Error CannotCancelTransfer =
      new("Transfer.CannotCancel", "Transfer cannot be Canceled ", StatusCodes.Status400BadRequest);
	
	public static readonly Error CannotCompletedTransfer =
      new("Transfer.CannotCompleted", "Transfer cannot be Completed ", StatusCodes.Status400BadRequest);

}
