using Microsoft.EntityFrameworkCore;
using StockPlatform.API.Models;

namespace StockPlatform.API.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Financial> Financials => Set<Financial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Company indexes
        modelBuilder.Entity<Company>()
            .HasIndex(c => c.Ticker)
            .IsUnique();

        modelBuilder.Entity<Company>()
            .HasIndex(c => new { c.Sector, c.MarketCap });

        // Financial composite index
        modelBuilder.Entity<Financial>()
            .HasIndex(f => new { f.CompanyId, f.ReportDate });

        base.OnModelCreating(modelBuilder);
    }
}
