using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPlatform.API.Models;

[Table("companies")]
public class Company
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Required]
    [MaxLength(10)]
    [Column("ticker")]
    public string Ticker { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("sector_id")]
    public int? SectorId { get; set; }

    [MaxLength(100)]
    [Column("industry")]
    public string? Industry { get; set; }

    [Column("market_cap")]
    public decimal? MarketCap { get; set; }

    [MaxLength(50)]
    [Column("country")]
    public string? Country { get; set; } = "US";

    [MaxLength(20)]
    [Column("exchange")]
    public string? Exchange { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("SectorId")]
    public Sector? Sector { get; set; }

    public ICollection<Financial> Financials { get; set; } = new List<Financial>();
    public ICollection<StockPrice> StockPrices { get; set; } = new List<StockPrice>();
    public ICollection<MlPrediction> MlPredictions { get; set; } = new List<MlPrediction>();
}
