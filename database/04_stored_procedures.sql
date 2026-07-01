-- ============================================================
-- STORED PROCEDURES
-- ============================================================

-- 1. Get full company profile (used by company detail page)
CREATE OR REPLACE FUNCTION get_company_profile(p_ticker VARCHAR)
RETURNS TABLE (
    company_id   INT,
    ticker       VARCHAR,
    name         VARCHAR,
    sector       VARCHAR,
    industry     VARCHAR,
    market_cap   NUMERIC,
    fiscal_year  INT,
    revenue      NUMERIC,
    net_income   NUMERIC,
    eps          NUMERIC,
    roe          NUMERIC,
    debt_to_equity NUMERIC,
    revenue_growth NUMERIC,
    latest_price NUMERIC,
    price_date   DATE
) AS $$
BEGIN
    RETURN QUERY
    SELECT * FROM mv_company_summary
    WHERE mv_company_summary.ticker = UPPER(p_ticker);
END;
$$ LANGUAGE plpgsql;

-- 2. Get top N companies by ROE within a sector
CREATE OR REPLACE FUNCTION get_top_companies_by_roe(
    p_sector VARCHAR,
    p_limit  INT DEFAULT 10
)
RETURNS TABLE (
    ticker       VARCHAR,
    name         VARCHAR,
    roe          NUMERIC,
    revenue      NUMERIC,
    profit_margin NUMERIC,
    market_cap   NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        mv.ticker,
        mv.name,
        mv.roe,
        mv.revenue,
        mv.profit_margin,
        mv.market_cap
    FROM mv_company_summary mv
    WHERE mv.sector = p_sector
      AND mv.roe IS NOT NULL
    ORDER BY mv.roe DESC
    LIMIT p_limit;
END;
$$ LANGUAGE plpgsql;

-- 3. Get financial history for a company (used for charts)
CREATE OR REPLACE FUNCTION get_financial_history(
    p_company_id INT,
    p_years      INT DEFAULT 5
)
RETURNS TABLE (
    fiscal_year    INT,
    revenue        NUMERIC,
    net_income     NUMERIC,
    eps            NUMERIC,
    roe            NUMERIC,
    revenue_growth NUMERIC,
    profit_margin  NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        f.fiscal_year,
        f.revenue,
        f.net_income,
        f.eps,
        f.roe,
        f.revenue_growth,
        f.profit_margin
    FROM financials f
    WHERE f.company_id = p_company_id
      AND f.period_type = 'annual'
    ORDER BY f.fiscal_year DESC
    LIMIT p_years;
END;
$$ LANGUAGE plpgsql;

-- 4. Upsert financials (used by Python ETL)
CREATE OR REPLACE PROCEDURE upsert_financial(
    p_company_id     INT,
    p_report_date    DATE,
    p_fiscal_year    INT,
    p_fiscal_quarter SMALLINT,
    p_period_type    VARCHAR,
    p_revenue        NUMERIC,
    p_net_income     NUMERIC,
    p_eps            NUMERIC,
    p_roe            NUMERIC,
    p_roa            NUMERIC,
    p_debt_to_equity NUMERIC,
    p_revenue_growth NUMERIC,
    p_profit_margin  NUMERIC
)
LANGUAGE plpgsql AS $$
BEGIN
    INSERT INTO financials (
        company_id, report_date, fiscal_year, fiscal_quarter,
        period_type, revenue, net_income, eps, roe, roa,
        debt_to_equity, revenue_growth, profit_margin
    )
    VALUES (
        p_company_id, p_report_date, p_fiscal_year, p_fiscal_quarter,
        p_period_type, p_revenue, p_net_income, p_eps, p_roe, p_roa,
        p_debt_to_equity, p_revenue_growth, p_profit_margin
    )
    ON CONFLICT (company_id, report_date)
    DO UPDATE SET
        revenue        = EXCLUDED.revenue,
        net_income     = EXCLUDED.net_income,
        eps            = EXCLUDED.eps,
        roe            = EXCLUDED.roe,
        roa            = EXCLUDED.roa,
        debt_to_equity = EXCLUDED.debt_to_equity,
        revenue_growth = EXCLUDED.revenue_growth,
        profit_margin  = EXCLUDED.profit_margin;
END;
$$;

-- 5. Get ML training dataset
CREATE OR REPLACE FUNCTION get_ml_training_data()
RETURNS TABLE (
    company_id     INT,
    ticker         VARCHAR,
    sector         VARCHAR,
    fiscal_year    INT,
    revenue_growth NUMERIC,
    roe            NUMERIC,
    roa            NUMERIC,
    debt_to_equity NUMERIC,
    profit_margin  NUMERIC,
    prev_revenue   NUMERIC,
    prev_net_income NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        company_id,
        ticker,
        sector,
        fiscal_year,
        revenue_growth,
        roe,
        roa,
        debt_to_equity,
        profit_margin,
        prev_year_revenue,
        prev_year_net_income
    FROM mv_growth_trends
    WHERE revenue_growth IS NOT NULL
      AND roe IS NOT NULL;
END;
$$ LANGUAGE plpgsql;
