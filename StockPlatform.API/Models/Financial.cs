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

    [Column("revenue")]
    public decimal? Revenue { get; set; }

    [Column("net_income")]
    public decimal? NetIncome { get; set; }

    [Column("eps")]
    public decimal? Eps { get; set; }

    [Column("roe")]
    public decimal? Roe { get; set; }

    [Column("debt_to_equity")]
    public decimal? DebtToEquity { get; set; }

    [Column("revenue_growth")]
    public decimal? RevenueGrowth { get; set; }

    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
}
