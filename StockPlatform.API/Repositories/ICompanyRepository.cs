using StockPlatform.API.Models;

namespace StockPlatform.API.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(int id);
    Task<Company?> GetByTickerAsync(string ticker);
    Task<Company> CreateAsync(Company company);
    Task<bool> DeleteAsync(int id);
}
