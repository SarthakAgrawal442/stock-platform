namespace StockPlatform.API.DTOs;

public record CompanyDto(
    int CompanyId,
    string Ticker,
    string Name,
    string? Sector,
    string? Industry,
    decimal? MarketCap
);

public record CreateCompanyDto(
    string Ticker,
    string Name,
    string? Sector,
    string? Industry,
    decimal? MarketCap
);
