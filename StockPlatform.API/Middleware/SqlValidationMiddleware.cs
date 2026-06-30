namespace StockPlatform.API.Middleware;

public static class SqlValidator
{
    private static readonly string[] AllowedTables = { "companies", "financials", "metrics" };
    private static readonly string[] BlockedKeywords = { "drop", "delete", "update", "insert", "exec", "xp_", "--", ";" };

    public static (bool IsValid, string Reason) Validate(string sql)
    {
        var lower = sql.Trim().ToLower();

        if (!lower.StartsWith("select"))
            return (false, "Only SELECT statements are allowed.");

        foreach (var keyword in BlockedKeywords)
            if (lower.Contains(keyword))
                return (false, $"Blocked keyword detected: {keyword}");

        bool hasAllowedTable = AllowedTables.Any(t => lower.Contains(t));
        if (!hasAllowedTable)
            return (false, "SQL references no allowed tables.");

        return (true, string.Empty);
    }

    public static string InjectLimit(string sql, int limit = 500)
    {
        var lower = sql.Trim().ToLower();
        if (!lower.Contains("limit"))
            sql += $" LIMIT {limit}";
        return sql;
    }
}
