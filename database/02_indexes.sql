-- ============================================================
-- INDEXES
-- ============================================================

-- Companies
CREATE UNIQUE INDEX idx_companies_ticker     ON companies(ticker);
CREATE INDEX idx_companies_sector            ON companies(sector_id);
CREATE INDEX idx_companies_market_cap        ON companies(market_cap DESC);
CREATE INDEX idx_companies_active            ON companies(is_active) WHERE is_active = TRUE;

-- Composite: sector + market cap (common dashboard filter)
CREATE INDEX idx_companies_sector_cap        ON companies(sector_id, market_cap DESC);

-- Trigram index for fuzzy company name search
CREATE INDEX idx_companies_name_trgm         ON companies USING GIN (name gin_trgm_ops);
CREATE INDEX idx_companies_ticker_trgm       ON companies USING GIN (ticker gin_trgm_ops);

-- Financials
CREATE INDEX idx_financials_company          ON financials(company_id);
CREATE INDEX idx_financials_date             ON financials(report_date DESC);
CREATE INDEX idx_financials_year             ON financials(fiscal_year);

-- Composite: company + date (most common query pattern)
CREATE INDEX idx_financials_company_date     ON financials(company_id, report_date DESC);

-- Composite: year + period type (ETL + reporting)
CREATE INDEX idx_financials_year_period      ON financials(fiscal_year, period_type);

-- Stock Prices
CREATE INDEX idx_prices_company              ON stock_prices(company_id);
CREATE INDEX idx_prices_date                 ON stock_prices(price_date DESC);
CREATE INDEX idx_prices_company_date         ON stock_prices(company_id, price_date DESC);

-- ML Predictions
CREATE INDEX idx_predictions_company         ON ml_predictions(company_id);
CREATE INDEX idx_predictions_classification  ON ml_predictions(classification);
CREATE INDEX idx_predictions_predicted_at    ON ml_predictions(predicted_at DESC);
