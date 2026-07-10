using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockPlatform.API.Models;

[Table("financials")]
public class Financial
{
    [Key]
    [Column("financial_id")]
    public int FinancialId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("report_date")]
    public DateOnly ReportDate { get; set; }

    [Column("fiscal_year")]
    public int FiscalYear { get; set; }

    [Column("fiscal_quarter")]
    public short? FiscalQuarter { get; set; }

    [MaxLength(10)]
    [Column("period_type")]
    public string? PeriodType { get; set; }

    [Column("revenue")]
    public decimal? Revenue { get; set; }

    [Column("gross_profit")]
    public decimal? GrossProfit { get; set; }

    [Column("operating_income")]
    public decimal? OperatingIncome { get; set; }

    [Column("net_income")]
    public decimal? NetIncome { get; set; }

    [Column("eps")]
    public decimal? Eps { get; set; }

    [Column("ebitda")]
    public decimal? Ebitda { get; set; }

    [Column("total_assets")]
    public decimal? TotalAssets { get; set; }

    [Column("total_liabilities")]
    public decimal? TotalLiabilities { get; set; }

    [Column("total_equity")]
    public decimal? TotalEquity { get; set; }

    [Column("cash")]
    public decimal? Cash { get; set; }

    [Column("total_debt")]
    public decimal? TotalDebt { get; set; }

    [Column("roe")]
    public decimal? Roe { get; set; }

    [Column("roa")]
    public decimal? Roa { get; set; }

    [Column("debt_to_equity")]
    public decimal? DebtToEquity { get; set; }

    [Column("current_ratio")]
    public decimal? CurrentRatio { get; set; }

    [Column("profit_margin")]
    public decimal? ProfitMargin { get; set; }

    [Column("revenue_growth")]
    public decimal? RevenueGrowth { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
}
