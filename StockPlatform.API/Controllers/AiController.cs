using Microsoft.AspNetCore.Mvc;
using StockPlatform.API.DTOs;
using StockPlatform.API.Middleware;
using StockPlatform.API.Services;

namespace StockPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{
    private readonly IPythonAiService _ai;

    public AiController(IPythonAiService ai)
    {
        _ai = ai;
    }

    // POST api/ai/predict
    [HttpPost("predict")]
    public async Task<IActionResult> Predict([FromBody] PredictionRequestDto request)
    {
        var result = await _ai.GetPredictionAsync(request.CompanyId);
        if (result is null)
            return StatusCode(503, "AI service unavailable.");
        return Ok(result);
    }

    // POST api/ai/query
    [HttpPost("query")]
    public async Task<IActionResult> NaturalLanguageQuery([FromBody] LlmSqlRequestDto request)
    {
        var generated = await _ai.GenerateSqlAsync(request.Query);
        if (generated is null)
            return StatusCode(503, "AI service unavailable.");

        var (isValid, reason) = SqlValidator.Validate(generated.Sql);
        if (!isValid)
            return BadRequest(new { error = "Generated SQL failed validation.", reason });

        var safeSql = SqlValidator.InjectLimit(generated.Sql);

        // TODO: execute safeSql via Dapper raw query and return results
        return Ok(new { sql = safeSql, tablesUsed = generated.TablesUsed });
    }

    // GET api/ai/health
    [HttpGet("health")]
    public async Task<IActionResult> Health()
    {
        var healthy = await _ai.IsHealthyAsync();
        return healthy ? Ok(new { status = "ok" }) : StatusCode(503, new { status = "unavailable" });
    }
}
