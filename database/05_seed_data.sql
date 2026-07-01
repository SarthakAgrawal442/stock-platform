-- ============================================================
-- SEED DATA
-- ============================================================

-- Sectors
INSERT INTO sectors (name, description) VALUES
('Technology',        'Software, hardware, semiconductors, and IT services'),
('Healthcare',        'Pharmaceuticals, biotech, medical devices, and health services'),
('Financials',        'Banks, insurance, asset management, and fintech'),
('Consumer Cyclical', 'Retail, automotive, travel, and discretionary spending'),
('Energy',            'Oil, gas, renewables, and energy services'),
('Industrials',       'Aerospace, defense, machinery, and transportation'),
('Communication',     'Telecom, media, and internet services'),
('Consumer Staples',  'Food, beverages, household products'),
('Utilities',         'Electric, gas, and water utilities'),
('Real Estate',       'REITs and real estate services');

-- Companies
INSERT INTO companies (ticker, name, sector_id, industry, market_cap, exchange) VALUES
-- Technology
('AAPL',  'Apple Inc.',             1, 'Consumer Electronics',    3100000000000, 'NASDAQ'),
('MSFT',  'Microsoft Corporation',  1, 'Software',                3000000000000, 'NASDAQ'),
('NVDA',  'NVIDIA Corporation',     1, 'Semiconductors',          2900000000000, 'NASDAQ'),
('GOOGL', 'Alphabet Inc.',          1, 'Internet Services',       2100000000000, 'NASDAQ'),
('META',  'Meta Platforms Inc.',    1, 'Social Media',            1300000000000, 'NASDAQ'),
('AMD',   'Advanced Micro Devices', 1, 'Semiconductors',           250000000000, 'NASDAQ'),
('INTC',  'Intel Corporation',      1, 'Semiconductors',           100000000000, 'NASDAQ'),
-- Healthcare
('JNJ',   'Johnson & Johnson',      2, 'Pharmaceuticals',          380000000000, 'NYSE'),
('UNH',   'UnitedHealth Group',     2, 'Health Insurance',         450000000000, 'NYSE'),
('PFE',   'Pfizer Inc.',            2, 'Pharmaceuticals',          160000000000, 'NYSE'),
-- Financials
('JPM',   'JPMorgan Chase',         3, 'Banking',                  580000000000, 'NYSE'),
('BAC',   'Bank of America',        3, 'Banking',                  300000000000, 'NYSE'),
('V',     'Visa Inc.',              3, 'Payments',                 550000000000, 'NYSE'),
-- Consumer
('AMZN',  'Amazon.com Inc.',        4, 'E-Commerce',              1900000000000, 'NASDAQ'),
('TSLA',  'Tesla Inc.',             4, 'Electric Vehicles',        800000000000, 'NASDAQ'),
-- Energy
('XOM',   'Exxon Mobil',            5, 'Oil & Gas',                480000000000, 'NYSE'),
('CVX',   'Chevron Corporation',    5, 'Oil & Gas',                280000000000, 'NYSE');

-- Financials - Apple (annual, 2020-2024)
INSERT INTO financials (
    company_id, report_date, fiscal_year, fiscal_quarter, period_type,
    revenue, gross_profit, net_income, eps, ebitda,
    roe, roa, debt_to_equity, profit_margin, revenue_growth
) VALUES
(1, '2020-09-30', 2020, 4, 'annual', 274515000000, 104956000000,  57411000000, 3.28, 77344000000, 0.7369, 0.1736, 1.7268, 0.2091, 0.0551),
(1, '2021-09-30', 2021, 4, 'annual', 365817000000, 152836000000,  94680000000, 5.61, 120233000000, 1.4974, 0.2696, 1.9867, 0.2588, 0.3326),
(1, '2022-09-30', 2022, 4, 'annual', 394328000000, 170782000000,  99803000000, 6.11, 130541000000, 1.9743, 0.2824, 1.9566, 0.2531, 0.0780),
(1, '2023-09-30', 2023, 4, 'annual', 383285000000, 169148000000,  96995000000, 6.13, 125820000000, 1.7195, 0.2785, 1.7962, 0.2531,-0.0280),
(1, '2024-09-30', 2024, 4, 'annual', 391035000000, 180683000000, 101956000000, 6.57, 134690000000, 1.6466, 0.2822, 1.5121, 0.2608, 0.0202);

-- Financials - Microsoft (annual, 2020-2024)
INSERT INTO financials (
    company_id, report_date, fiscal_year, fiscal_quarter, period_type,
    revenue, net_income, eps, roe, roa, debt_to_equity, profit_margin, revenue_growth
) VALUES
(2, '2020-06-30', 2020, 4, 'annual', 143015000000, 44281000000, 5.76, 0.4039, 0.1545, 0.6302, 0.3096, 0.1319),
(2, '2021-06-30', 2021, 4, 'annual', 168088000000, 61271000000, 8.05, 0.4352, 0.1873, 0.5535, 0.3645, 0.1753),
(2, '2022-06-30', 2022, 4, 'annual', 198270000000, 72738000000, 9.65, 0.4355, 0.2003, 0.4843, 0.3668, 0.1796),
(2, '2023-06-30', 2023, 4, 'annual', 211915000000, 72361000000, 9.72, 0.3500, 0.1835, 0.3780, 0.3415, 0.0688),
(2, '2024-06-30', 2024, 4, 'annual', 245122000000, 88136000000, 11.80, 0.3847, 0.1878, 0.3668, 0.3597, 0.1567);

-- Financials - NVIDIA (annual, 2020-2024)
INSERT INTO financials (
    company_id, report_date, fiscal_year, fiscal_quarter, period_type,
    revenue, net_income, eps, roe, roa, debt_to_equity, profit_margin, revenue_growth
) VALUES
(3, '2020-01-31', 2020, 4, 'annual',  10918000000,  2796000000, 1.13, 0.2344, 0.1354, 0.3978, 0.2561, 0.1558),
(3, '2021-01-31', 2021, 4, 'annual',  16675000000,  4332000000, 1.73, 0.3140, 0.1747, 0.3999, 0.2598, 0.5272),
(3, '2022-01-31', 2022, 4, 'annual',  26914000000,  9752000000, 3.85, 0.4473, 0.2802, 0.4106, 0.3624, 0.6140),
(3, '2023-01-31', 2023, 4, 'annual',  26974000000,  4368000000, 1.74, 0.1812, 0.0723, 0.4107, 0.1619,-0.0022),
(3, '2024-01-31', 2024, 4, 'annual',  60922000000, 29760000000, 11.93, 0.9145, 0.4513, 0.4133, 0.4885, 1.2584);
