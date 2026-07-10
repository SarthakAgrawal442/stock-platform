using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPlatform.API.Models;

[Table("stock_prices")]
public class StockPrice
{
    [Key]
    [Column("price_id")]
    public long PriceId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("price_date")]
    public DateOnly PriceDate { get; set; }

    [Column("open_price")]
    public decimal? OpenPrice { get; set; }

    [Column("high_price")]
    public decimal? HighPrice { get; set; }

    [Column("low_price")]
    public decimal? LowPrice { get; set; }

    [Column("close_price")]
    public decimal? ClosePrice { get; set; }

    [Column("adj_close")]
    public decimal? AdjClose { get; set; }

    [Column("volume")]
    public long? Volume { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
}
