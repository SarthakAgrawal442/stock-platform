-- ============================================================
-- RUN ALL - Execute in order
-- ============================================================
-- Run this file to set up the full database from scratch:
--   psql -U postgres -d stockplatform -f database/06_run_all.sql

\i database/01_schema.sql
\i database/02_indexes.sql
\i database/03_materialized_views.sql
\i database/04_stored_procedures.sql
\i database/05_seed_data.sql

-- Populate materialized views with seed data
SELECT refresh_all_views();

-- Verify
SELECT 'sectors'    AS table_name, COUNT(*) FROM sectors    UNION ALL
SELECT 'companies'  AS table_name, COUNT(*) FROM companies  UNION ALL
SELECT 'financials' AS table_name, COUNT(*) FROM financials UNION ALL
SELECT 'mv_company_summary' AS table_name, COUNT(*) FROM mv_company_summary;
