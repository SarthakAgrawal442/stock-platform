using Microsoft.AspNetCore.Mvc;
using StockPlatform.API.DTOs;
using StockPlatform.API.Services;

namespace StockPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _service;

    public CompaniesController(ICompanyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var company = await _service.GetByIdAsync(id);
        return company is null ? NotFound() : Ok(company);
    }

    [HttpGet("ticker/{ticker}")]
    public async Task<IActionResult> GetByTicker(string ticker)
    {
        var company = await _service.GetByTickerAsync(ticker);
        return company is null ? NotFound() : Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.CompanyId }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
