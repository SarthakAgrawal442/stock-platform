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

    [MaxLength(100)]
    [Column("sector")]
    public string? Sector { get; set; }

    [MaxLength(100)]
    [Column("industry")]
    public string? Industry { get; set; }

    [Column("market_cap")]
    public decimal? MarketCap { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Financial> Financials { get; set; } = new List<Financial>();
}
