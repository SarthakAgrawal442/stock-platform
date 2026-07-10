namespace StockPlatform.API.DTOs;

public record CompanyDto(
    int CompanyId,
    string Ticker,
    string Name,
    int? SectorId,
    string? SectorName,
    string? Industry,
    decimal? MarketCap
);

public record CreateCompanyDto(
    string Ticker,
    string Name,
    int? SectorId,
    string? Industry,
    decimal? MarketCap
);
