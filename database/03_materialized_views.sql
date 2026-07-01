-- ============================================================
-- MATERIALIZED VIEWS
-- ============================================================

-- 1. Company Financial Summary (latest annual data per company)
CREATE MATERIALIZED VIEW mv_company_summary AS
SELECT
    c.company_id,
    c.ticker,
    c.name,
    s.name         AS sector,
    c.industry,
    c.market_cap,
    f.fiscal_year,
    f.revenue,
    f.net_income,
    f.eps,
    f.roe,
    f.roa,
    f.debt_to_equity,
    f.profit_margin,
    f.revenue_growth,
    f.ebitda,
    p.latest_price,
    p.price_date
FROM companies c
JOIN sectors s ON c.sector_id = s.sector_id
JOIN LATERAL (
    SELECT *
    FROM financials
    WHERE company_id = c.company_id
      AND period_type = 'annual'
    ORDER BY report_date DESC
    LIMIT 1
) f ON TRUE
LEFT JOIN LATERAL (
    SELECT close_price AS latest_price, price_date
    FROM stock_prices
    WHERE company_id = c.company_id
    ORDER BY price_date DESC
    LIMIT 1
) p ON TRUE
WHERE c.is_active = TRUE;

CREATE UNIQUE INDEX idx_mv_company_summary ON mv_company_summary(company_id);
CREATE INDEX idx_mv_summary_sector         ON mv_company_summary(sector);
CREATE INDEX idx_mv_summary_roe            ON mv_company_summary(roe DESC);

-- 2. Sector Aggregates (for sector comparison charts)
CREATE MATERIALIZED VIEW mv_sector_aggregates AS
SELECT
    s.name                          AS sector,
    COUNT(c.company_id)             AS company_count,
    AVG(f.revenue)                  AS avg_revenue,
    AVG(f.profit_margin)            AS avg_profit_margin,
    AVG(f.roe)                      AS avg_roe,
    AVG(f.revenue_growth)           AS avg_revenue_growth,
    AVG(f.debt_to_equity)           AS avg_debt_to_equity,
    SUM(c.market_cap)               AS total_market_cap
FROM sectors s
JOIN companies c ON c.sector_id = s.sector_id
JOIN LATERAL (
    SELECT *
    FROM financials
    WHERE company_id = c.company_id
      AND period_type = 'annual'
    ORDER BY report_date DESC
    LIMIT 1
) f ON TRUE
WHERE c.is_active = TRUE
GROUP BY s.name;

CREATE UNIQUE INDEX idx_mv_sector_agg ON mv_sector_aggregates(sector);

-- 3. Revenue Growth Trend (for ML training dataset)
CREATE MATERIALIZED VIEW mv_growth_trends AS
SELECT
    c.company_id,
    c.ticker,
    s.name          AS sector,
    f.fiscal_year,
    f.revenue,
    f.net_income,
    f.roe,
    f.roa,
    f.debt_to_equity,
    f.revenue_growth,
    f.profit_margin,
    LAG(f.revenue, 1) OVER (PARTITION BY c.company_id ORDER BY f.fiscal_year) AS prev_year_revenue,
    LAG(f.net_income, 1) OVER (PARTITION BY c.company_id ORDER BY f.fiscal_year) AS prev_year_net_income
FROM companies c
JOIN sectors s ON c.sector_id = s.sector_id
JOIN financials f ON f.company_id = c.company_id AND f.period_type = 'annual'
WHERE c.is_active = TRUE
ORDER BY c.company_id, f.fiscal_year;

CREATE INDEX idx_mv_growth_company ON mv_growth_trends(company_id);
CREATE INDEX idx_mv_growth_sector  ON mv_growth_trends(sector);
CREATE INDEX idx_mv_growth_year    ON mv_growth_trends(fiscal_year);

-- Refresh function (called after ETL runs)
CREATE OR REPLACE FUNCTION refresh_all_views()
RETURNS VOID AS $$
BEGIN
    REFRESH MATERIALIZED VIEW CONCURRENTLY mv_company_summary;
    REFRESH MATERIALIZED VIEW CONCURRENTLY mv_sector_aggregates;
    REFRESH MATERIALIZED VIEW CONCURRENTLY mv_growth_trends;
END;
$$ LANGUAGE plpgsql;
