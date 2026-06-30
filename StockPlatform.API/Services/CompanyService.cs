using StockPlatform.API.DTOs;
using StockPlatform.API.Models;
using StockPlatform.API.Repositories;

namespace StockPlatform.API.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repo;

    public CompanyService(ICompanyRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _repo.GetAllAsync();
        return companies.Select(ToDto);
    }

    public async Task<CompanyDto?> GetByIdAsync(int id)
    {
        var company = await _repo.GetByIdAsync(id);
        return company is null ? null : ToDto(company);
    }

    public async Task<CompanyDto?> GetByTickerAsync(string ticker)
    {
        var company = await _repo.GetByTickerAsync(ticker);
        return company is null ? null : ToDto(company);
    }

    public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto)
    {
        var company = new Company
        {
            Ticker = dto.Ticker.ToUpper(),
            Name = dto.Name,
            Sector = dto.Sector,
            Industry = dto.Industry,
            MarketCap = dto.MarketCap
        };
        var created = await _repo.CreateAsync(company);
        return ToDto(created);
    }

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

    private static CompanyDto ToDto(Company c) => new(
        c.CompanyId, c.Ticker, c.Name, c.Sector, c.Industry, c.MarketCap
    );
}
