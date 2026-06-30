using StockPlatform.API.Models;

namespace StockPlatform.API.Repositories;

public interface IFinancialRepository
{
    Task<IEnumerable<Financial>> GetByCompanyIdAsync(int companyId);
    Task<Financial> CreateAsync(Financial financial);
}
