using FinancialTransfers.Application.Contracts.Account;
using FinancialTransfers.Application.Contracts.Common;
using FinancialTransfers.Application.Contracts.Transfer;

namespace FinancialTransfers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransfersController(ITransferService transferService) : ControllerBase
{
	private readonly ITransferService _transferService = transferService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _transferService.GetAsync(id, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("")]
	public async Task<IActionResult> GetAll([FromQuery] RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var result = await _transferService.GetAllAsync(filters, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
	[HttpPost("")]
	public async Task<IActionResult> AddAsync([FromBody] TransferRequest request, CancellationToken cancellationToken = default)
	{
		var result = await _transferService.AddAsync(request, cancellationToken);
	        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}/complete")]
	public async Task<IActionResult> ToggleCompleted([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _transferService.ToggleAsCompletedAsync(id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/cancel")]
	public async Task<IActionResult> ToggleCancelled([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _transferService.ToggleAsCancelledAsync(id, cancellationToken);
		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpGet("summary")]
	public async Task<IActionResult> GetSummary( CancellationToken cancellationToken)
	{
		var result = await _transferService.GetSummaryAsync( cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

}
