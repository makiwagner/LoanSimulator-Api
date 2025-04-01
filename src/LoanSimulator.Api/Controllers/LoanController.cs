using LoanSimulator.Application.DTOs;
using LoanSimulator.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulator.Api.Controllers;

[Route("api/loans")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly LoanApplicationService _loanApplicationService;

    public LoanController(LoanApplicationService loanApplicationService)
    {
        _loanApplicationService = loanApplicationService;
    }

    [HttpPost("simulate")]
    public async Task<IActionResult> Simulate([FromBody] LoanRequestDto request)
    {
        var response = await _loanApplicationService.SimulateLoanAsync(request);
        return Ok(response);
    }
}
