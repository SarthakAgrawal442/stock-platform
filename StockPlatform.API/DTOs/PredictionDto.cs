namespace StockPlatform.API.DTOs;

public record PredictionRequestDto(int CompanyId);

public record PredictionResponseDto(
    string Classification,
    double Confidence,
    List<string> TopFeatures
);

public record LlmSqlRequestDto(string Query);

public record LlmSqlResponseDto(string Sql, List<string> TablesUsed);
