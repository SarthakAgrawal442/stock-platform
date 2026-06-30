# Stock Platform

A full-stack financial advisor platform built as a database-focused capstone project. It combines a normalized PostgreSQL data warehouse, an ASP.NET Core REST API, a Python AI/data engine, and a React dashboard.

---

## Architecture

```
React + TypeScript
       │
  REST API Calls
       │
 ASP.NET Core Web API
       │
Entity Framework Core / Npgsql
       │
  PostgreSQL Database
  ├── Materialized Views
  ├── Stored Procedures
  ├── Indexes
  ├── Partitioning
  └── ETL Tables
       │
  Python AI Service (FastAPI)
  ├── ETL Pipeline
  ├── Feature Engineering
  ├── ML Model (Random Forest)
  ├── LLM SQL Generator
  └── Model Training
```

> ASP.NET Core never talks directly to Yahoo Finance or the LLM. It only communicates with PostgreSQL and the Python AI service.

---

## Tech Stack

### Frontend
- React + TypeScript
- Tailwind CSS
- Recharts / Chart.js

### Backend
- ASP.NET Core 9 Web API
- Entity Framework Core
- Npgsql
- Polly (circuit breaker for Python service)

### Database (PostgreSQL)
- Normalization
- Indexes + Composite Indexes
- Stored Procedures
- Materialized Views
- Transactions
- Partitioning

### Python AI Service (FastAPI)
- ETL Pipeline (Yahoo Finance → PostgreSQL)
- Feature Engineering
- Random Forest ML Classifier
- LLM → SQL Generator (OpenAI or local model)
- Saved model as `.pkl`

---

## Folder Structure

```
stock-platform/
├── StockPlatform.API/
│   ├── Controllers/
│   ├── Services/
│   ├── Repositories/
│   ├── Models/
│   ├── DTOs/
│   ├── Middleware/
│   │   ├── SqlValidationMiddleware.cs
│   │   └── PythonServiceCircuitBreaker.cs
│   ├── Database/
│   └── Program.cs
│
├── python-service/
│   ├── api/
│   │   ├── main.py
│   │   └── routes/
│   │       ├── predict.py
│   │       ├── etl.py
│   │       └── llm.py
│   ├── etl/
│   ├── ml/
│   │   ├── train.py
│   │   ├── predict.py
│   │   └── models/
│   ├── llm/
│   │   ├── sql_generator.py
│   │   └── prompt_templates/
│   ├── utils/
│   ├── requirements.txt
│   └── Dockerfile
│
├── react-frontend/
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── hooks/
│   │   └── services/
│   └── package.json
│
├── docker-compose.yml
└── README.md
```

---

## API Communication

### ML Prediction
```
POST /api/ml/predict
{ "companyId": 105 }

→ {
    "classification": "High Growth",
    "confidence": 0.93,
    "topFeatures": ["Revenue Growth", "ROE", "Low Debt"]
  }
```

### LLM SQL Generation
```
POST /api/llm/generate-sql
{ "query": "Find profitable semiconductor companies with growing revenue" }

→ {
    "sql": "SELECT ...",
    "tables_used": ["companies", "financials"]
  }
```

> AI-generated SQL is never executed directly. ASP.NET validates it against a whitelist (SELECT only, known tables, no DROP/DELETE/UPDATE).

### Python Service Health Check
```
GET /health
→ { "status": "ok", "model_loaded": true }
```

---

## ETL Pipeline

Runs nightly via scheduler:

```
Yahoo Finance → Download → Clean → Normalize → Load PostgreSQL → Refresh Materialized Views
```

---

## ML Workflow

Training (offline):
```
PostgreSQL → Training Dataset → Random Forest → Saved Model (.pkl)
```

Prediction (runtime):
```
React → ASP.NET → Python → Load Model → Prediction → Return Result
```

---

## LLM SQL Workflow

```
User: "Find profitable semiconductor companies with growing revenue"
→ ASP.NET → Python → OpenAI API → SQL
→ ASP.NET validates SQL → PostgreSQL → Results → React
```

**SQL Validation Rules:**
- Only `SELECT` statements allowed
- Whitelist of allowed tables enforced
- Blocked keywords: `DROP`, `DELETE`, `UPDATE`, `INSERT`, `EXEC`
- Auto-inject `LIMIT 500` if missing

---

## Running Locally

```bash
# Start all services
docker compose up

# Services
# React:        http://localhost:3000
# ASP.NET API:  http://localhost:5000
# Python API:   http://localhost:8000
# PostgreSQL:   localhost:5432
```

---

## Development Timeline

| Week | Focus | Deliverable |
|------|-------|-------------|
| 1-2 | DB Design | ER diagram, schema, seed data |
| 3-4 | ASP.NET Core | CRUD APIs, EF Core, Swagger |
| 5-6 | Python ETL + FastAPI | Nightly pipeline, `/health`, `/predict` |
| 7-8 | React Dashboard | Charts, search, company pages |
| 9-10 | ML Model | Random Forest classifier, `.pkl` model |
| 11 | LLM SQL + Validation | Chat UI, SQL guard middleware |
| 12 | Polish | Docker Compose, docs, demo prep |

---

## Why This Project

Most student stock projects are just `Yahoo API → React → Charts` with no real database engineering.

This project puts the database at the core:

```
Financial Data → ETL Pipeline → Normalized PostgreSQL
→ Materialized Views → Stored Procedures → Indexes
→ ML Classification → Natural Language SQL → React Dashboard
```

It demonstrates three distinct skill sets in one project:
- **Software Engineering** - ASP.NET Core
- **Database Engineering** - PostgreSQL
- **Data Engineering / AI** - Python + ML + LLM

---

## Author

**Sarthak Agrawal** - Computer Science Student  
[GitHub](https://github.com/SarthakAgrawal442)
