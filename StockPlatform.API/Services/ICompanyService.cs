using StockPlatform.API.DTOs;

namespace StockPlatform.API.Services;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<CompanyDto?> GetByIdAsync(int id);
    Task<CompanyDto?> GetByTickerAsync(string ticker);
    Task<CompanyDto> CreateAsync(CreateCompanyDto dto);
    Task<bool> DeleteAsync(int id);
}
