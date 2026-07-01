-- ============================================================
-- STOCK PLATFORM - PostgreSQL Schema
-- ============================================================

-- Enable extensions
CREATE EXTENSION IF NOT EXISTS pg_trgm; -- for fuzzy text search

-- ============================================================
-- CORE TABLES
-- ============================================================

CREATE TABLE sectors (
    sector_id   SERIAL PRIMARY KEY,
    name        VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    created_at  TIMESTAMP DEFAULT NOW()
);

CREATE TABLE companies (
    company_id   SERIAL PRIMARY KEY,
    ticker       VARCHAR(10)  NOT NULL UNIQUE,
    name         VARCHAR(255) NOT NULL,
    sector_id    INT REFERENCES sectors(sector_id),
    industry     VARCHAR(100),
    market_cap   NUMERIC(20, 2),
    country      VARCHAR(50) DEFAULT 'US',
    exchange     VARCHAR(20),
    is_active    BOOLEAN DEFAULT TRUE,
    created_at   TIMESTAMP DEFAULT NOW(),
    updated_at   TIMESTAMP DEFAULT NOW()
);

-- Partitioned by year for performance demo
CREATE TABLE financials (
    financial_id   SERIAL,
    company_id     INT NOT NULL REFERENCES companies(company_id),
    report_date    DATE NOT NULL,
    fiscal_year    INT NOT NULL,
    fiscal_quarter SMALLINT CHECK (fiscal_quarter BETWEEN 1 AND 4),
    period_type    VARCHAR(10) CHECK (period_type IN ('annual', 'quarterly')),

    -- Income Statement
    revenue            NUMERIC(20, 2),
    gross_profit       NUMERIC(20, 2),
    operating_income   NUMERIC(20, 2),
    net_income         NUMERIC(20, 2),
    eps                NUMERIC(10, 4),
    ebitda             NUMERIC(20, 2),

    -- Balance Sheet
    total_assets       NUMERIC(20, 2),
    total_liabilities  NUMERIC(20, 2),
    total_equity       NUMERIC(20, 2),
    cash               NUMERIC(20, 2),
    total_debt         NUMERIC(20, 2),

    -- Ratios (pre-computed for ML features)
    roe                NUMERIC(10, 4),  -- Return on Equity
    roa                NUMERIC(10, 4),  -- Return on Assets
    debt_to_equity     NUMERIC(10, 4),
    current_ratio      NUMERIC(10, 4),
    profit_margin      NUMERIC(10, 4),
    revenue_growth     NUMERIC(10, 4),  -- YoY %

    created_at         TIMESTAMP DEFAULT NOW(),

    PRIMARY KEY (financial_id, fiscal_year)
) PARTITION BY RANGE (fiscal_year);

-- Create yearly partitions
CREATE TABLE financials_2020 PARTITION OF financials FOR VALUES FROM (2020) TO (2021);
CREATE TABLE financials_2021 PARTITION OF financials FOR VALUES FROM (2021) TO (2022);
CREATE TABLE financials_2022 PARTITION OF financials FOR VALUES FROM (2022) TO (2023);
CREATE TABLE financials_2023 PARTITION OF financials FOR VALUES FROM (2023) TO (2024);
CREATE TABLE financials_2024 PARTITION OF financials FOR VALUES FROM (2024) TO (2025);
CREATE TABLE financials_2025 PARTITION OF financials FOR VALUES FROM (2025) TO (2026);

CREATE TABLE stock_prices (
    price_id    BIGSERIAL PRIMARY KEY,
    company_id  INT NOT NULL REFERENCES companies(company_id),
    price_date  DATE NOT NULL,
    open_price  NUMERIC(12, 4),
    high_price  NUMERIC(12, 4),
    low_price   NUMERIC(12, 4),
    close_price NUMERIC(12, 4),
    adj_close   NUMERIC(12, 4),
    volume      BIGINT,
    created_at  TIMESTAMP DEFAULT NOW(),
    UNIQUE (company_id, price_date)
);

CREATE TABLE ml_predictions (
    prediction_id   SERIAL PRIMARY KEY,
    company_id      INT NOT NULL REFERENCES companies(company_id),
    model_version   VARCHAR(50),
    classification  VARCHAR(50),  -- High Growth, Stable, Declining
    confidence      NUMERIC(5, 4),
    top_features    JSONB,
    predicted_at    TIMESTAMP DEFAULT NOW()
);

CREATE TABLE etl_log (
    log_id      SERIAL PRIMARY KEY,
    run_date    TIMESTAMP DEFAULT NOW(),
    source      VARCHAR(50),
    status      VARCHAR(20) CHECK (status IN ('success', 'failed', 'partial')),
    rows_loaded INT,
    error_msg   TEXT
);
