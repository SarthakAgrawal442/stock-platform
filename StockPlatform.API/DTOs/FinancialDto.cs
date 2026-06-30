namespace StockPlatform.API.DTOs;

public record FinancialDto(
    int FinancialId,
    int CompanyId,
    DateOnly ReportDate,
    decimal? Revenue,
    decimal? NetIncome,
    decimal? Eps,
    decimal? Roe,
    decimal? DebtToEquity,
    decimal? RevenueGrowth
);
