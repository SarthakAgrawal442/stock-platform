using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPlatform.API.Models;

[Table("etl_log")]
public class EtlLog
{
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }

    [Column("run_date")]
    public DateTime RunDate { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    [Column("source")]
    public string? Source { get; set; }

    [MaxLength(20)]
    [Column("status")]
    public string? Status { get; set; }

    [Column("rows_loaded")]
    public int? RowsLoaded { get; set; }

    [Column("error_msg")]
    public string? ErrorMsg { get; set; }
}
