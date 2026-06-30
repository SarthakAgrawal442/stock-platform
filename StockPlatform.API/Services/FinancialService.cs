using StockPlatform.API.DTOs;
using StockPlatform.API.Models;
using StockPlatform.API.Repositories;

namespace StockPlatform.API.Services;

public class FinancialService : IFinancialService
{
    private readonly IFinancialRepository _repo;

    public FinancialService(IFinancialRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<FinancialDto>> GetByCompanyIdAsync(int companyId)
    {
        var records = await _repo.GetByCompanyIdAsync(companyId);
        return records.Select(ToDto);
    }

    public async Task<FinancialDto> CreateAsync(FinancialDto dto)
    {
        var financial = new Financial
        {
            CompanyId = dto.CompanyId,
            ReportDate = dto.ReportDate,
            Revenue = dto.Revenue,
            NetIncome = dto.NetIncome,
            Eps = dto.Eps,
            Roe = dto.Roe,
            DebtToEquity = dto.DebtToEquity,
            RevenueGrowth = dto.RevenueGrowth
        };
        var created = await _repo.CreateAsync(financial);
        return ToDto(created);
    }

    private static FinancialDto ToDto(Financial f) => new(
        f.FinancialId, f.CompanyId, f.ReportDate,
        f.Revenue, f.NetIncome, f.Eps, f.Roe, f.DebtToEquity, f.RevenueGrowth
    );
}
