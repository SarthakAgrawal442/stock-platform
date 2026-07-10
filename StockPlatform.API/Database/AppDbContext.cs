using Microsoft.EntityFrameworkCore;
using StockPlatform.API.Models;

namespace StockPlatform.API.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Sector> Sectors => Set<Sector>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Financial> Financials => Set<Financial>();
    public DbSet<StockPrice> StockPrices => Set<StockPrice>();
    public DbSet<MlPrediction> MlPredictions => Set<MlPrediction>();
    public DbSet<EtlLog> EtlLogs => Set<EtlLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sector
        modelBuilder.Entity<Sector>()
            .HasIndex(s => s.Name)
            .IsUnique();

        // Company
        modelBuilder.Entity<Company>()
            .HasIndex(c => c.Ticker)
            .IsUnique();

        modelBuilder.Entity<Company>()
            .HasOne(c => c.Sector)
            .WithMany(s => s.Companies)
            .HasForeignKey(c => c.SectorId)
            .OnDelete(DeleteBehavior.SetNull);

        // Financial - partitioned table, composite PK
        modelBuilder.Entity<Financial>()
            .HasKey(f => new { f.FinancialId, f.FiscalYear });

        modelBuilder.Entity<Financial>()
            .HasIndex(f => new { f.CompanyId, f.ReportDate });

        modelBuilder.Entity<Financial>()
            .HasOne(f => f.Company)
            .WithMany(c => c.Financials)
            .HasForeignKey(f => f.CompanyId);

        // StockPrice - unique per company+date
        modelBuilder.Entity<StockPrice>()
            .HasIndex(sp => new { sp.CompanyId, sp.PriceDate })
            .IsUnique();

        modelBuilder.Entity<StockPrice>()
            .HasOne(sp => sp.Company)
            .WithMany(c => c.StockPrices)
            .HasForeignKey(sp => sp.CompanyId);

        // MlPrediction
        modelBuilder.Entity<MlPrediction>()
            .HasOne(mp => mp.Company)
            .WithMany(c => c.MlPredictions)
            .HasForeignKey(mp => mp.CompanyId);

        base.OnModelCreating(modelBuilder);
    }
}
