using Microsoft.EntityFrameworkCore;
using StockPlatform.API.Database;
using StockPlatform.API.Models;

namespace StockPlatform.API.Repositories;

public class FinancialRepository : IFinancialRepository
{
    private readonly AppDbContext _context;

    public FinancialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Financial>> GetByCompanyIdAsync(int companyId) =>
        await _context.Financials
            .AsNoTracking()
            .Where(f => f.CompanyId == companyId)
            .OrderByDescending(f => f.ReportDate)
            .ToListAsync();

    public async Task<Financial> CreateAsync(Financial financial)
    {
        _context.Financials.Add(financial);
        await _context.SaveChangesAsync();
        return financial;
    }
}
