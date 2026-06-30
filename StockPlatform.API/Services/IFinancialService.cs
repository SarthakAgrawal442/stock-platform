using StockPlatform.API.DTOs;

namespace StockPlatform.API.Services;

public interface IFinancialService
{
    Task<IEnumerable<FinancialDto>> GetByCompanyIdAsync(int companyId);
    Task<FinancialDto> CreateAsync(FinancialDto dto);
}
