using System.Net.Http.Json;
using StockPlatform.API.DTOs;

namespace StockPlatform.API.Services;

public class PythonAiService : IPythonAiService
{
    private readonly HttpClient _http;

    public PythonAiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<PredictionResponseDto?> GetPredictionAsync(int companyId)
    {
        try
        {
            return await _http.PostAsJsonAsync("/api/ml/predict", new { companyId })
                .Result.Content.ReadFromJsonAsync<PredictionResponseDto>();
        }
        catch
        {
            return null; // circuit breaker fallback
        }
    }

    public async Task<LlmSqlResponseDto?> GenerateSqlAsync(string query)
    {
        try
        {
            return await _http.PostAsJsonAsync("/api/llm/generate-sql", new { query })
                .Result.Content.ReadFromJsonAsync<LlmSqlResponseDto>();
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> IsHealthyAsync()
    {
        try
        {
            var response = await _http.GetAsync("/health");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
