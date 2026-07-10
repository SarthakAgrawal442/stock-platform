using Microsoft.EntityFrameworkCore;
using StockPlatform.API.Database;
using StockPlatform.API.Models;

namespace StockPlatform.API.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllAsync() =>
        await _context.Companies
            .Include(c => c.Sector)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Company?> GetByIdAsync(int id) =>
        await _context.Companies
            .Include(c => c.Sector)
            .Include(c => c.Financials)
            .FirstOrDefaultAsync(c => c.CompanyId == id);

    public async Task<Company?> GetByTickerAsync(string ticker) =>
        await _context.Companies
            .Include(c => c.Sector)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Ticker == ticker.ToUpper());

    public async Task<Company> CreateAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company is null) return false;
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
        return true;
    }
}
