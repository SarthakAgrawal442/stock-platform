using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPlatform.API.Models;

[Table("ml_predictions")]
public class MlPrediction
{
    [Key]
    [Column("prediction_id")]
    public int PredictionId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [MaxLength(50)]
    [Column("model_version")]
    public string? ModelVersion { get; set; }

    [MaxLength(50)]
    [Column("classification")]
    public string? Classification { get; set; }

    [Column("confidence")]
    public decimal? Confidence { get; set; }

    [Column("top_features", TypeName = "jsonb")]
    public string? TopFeatures { get; set; }

    [Column("predicted_at")]
    public DateTime PredictedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
}
