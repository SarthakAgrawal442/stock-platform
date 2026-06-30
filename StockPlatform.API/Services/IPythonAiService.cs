using StockPlatform.API.DTOs;

namespace StockPlatform.API.Services;

public interface IPythonAiService
{
    Task<PredictionResponseDto?> GetPredictionAsync(int companyId);
    Task<LlmSqlResponseDto?> GenerateSqlAsync(string naturalLanguageQuery);
    Task<bool> IsHealthyAsync();
}
