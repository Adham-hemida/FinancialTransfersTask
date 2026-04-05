using FinancialTransfers.Application.Contracts.Account;
using FinancialTransfers.Application.Contracts.Common;

namespace FinancialTransfers.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountService accountService) : ControllerBase
{
	private readonly IAccountService _accountService = accountService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _accountService.GetAsync(id, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("")]
	public async Task<IActionResult> GetAll([FromQuery] RequestFilters filters, CancellationToken cancellationToken = default)
	{
		var result = await _accountService.GetAllAsync(filters, cancellationToken);
		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}	
	
	
	[HttpPost("")]
	public async Task<IActionResult> AddAsync([FromBody] AccountRequest request , CancellationToken cancellationToken = default)
	{
		var result = await _accountService.AddAsync(request,cancellationToken);
		return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

}
