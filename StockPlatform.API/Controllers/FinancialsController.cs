using Microsoft.AspNetCore.Mvc;
using StockPlatform.API.DTOs;
using StockPlatform.API.Services;

namespace StockPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinancialsController : ControllerBase
{
    private readonly IFinancialService _service;

    public FinancialsController(IFinancialService service)
    {
        _service = service;
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetByCompany(int companyId) =>
        Ok(await _service.GetByCompanyIdAsync(companyId));
}
