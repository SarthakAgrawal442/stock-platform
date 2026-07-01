# Stock Platform

A full-stack financial advisor platform built as a database-focused capstone project. It combines a normalized PostgreSQL data warehouse, an ASP.NET Core REST API, a Python AI/data engine, and a React dashboard.

![Status](https://img.shields.io/badge/API-Live-brightgreen) ![.NET](https://img.shields.io/badge/.NET-9.0-purple) ![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)

---

## Project Status

| Layer | Status | Notes |
|-------|--------|-------|
| ASP.NET Core API | ✅ Running | All CRUD endpoints live |
| PostgreSQL | ✅ Connected | EF Core migrations applied |
| EF Core Migrations | ✅ Done | `InitialCreate` applied |
| Scalar API Docs | ✅ Live | `http://localhost:5000/scalar/v1` |
| Python AI Service | 🔜 Week 5-6 | FastAPI + ETL |
| React Frontend | 🔜 Week 7-8 | Dashboard + Charts |
| ML Model | 🔜 Week 9-10 | Random Forest classifier |
| LLM SQL Generator | 🔜 Week 11 | Natural language to SQL |

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
- Entity Framework Core 9
- Npgsql 9
- Microsoft.Extensions.Http.Resilience (circuit breaker)
- Scalar (API docs)

### Database (PostgreSQL 16)
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

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Node.js 20+](https://nodejs.org/) (for React frontend - coming soon)

---

## Running Locally

### 1. Clone the repo
```bash
git clone https://github.com/SarthakAgrawal442/stock-platform.git
cd stock-platform
```

### 2. Start PostgreSQL
```bash
docker compose up postgres -d
```

### 3. Run the API
```bash
cd StockPlatform.API
dotnet restore
dotnet ef database update
dotnet run
```

### 4. Open API docs
```
http://localhost:5000/scalar/v1
```

### Start all services (once all layers are built)
```bash
docker compose up
```

| Service | URL |
|---------|-----|
| React Frontend | http://localhost:3000 |
| ASP.NET API | http://localhost:5000 |
| Scalar API Docs | http://localhost:5000/scalar/v1 |
| Python AI Service | http://localhost:8000 |
| PostgreSQL | localhost:5432 |

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
│   │   └── SqlValidationMiddleware.cs
│   ├── Database/
│   ├── Migrations/
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

## API Endpoints

### Companies
```
GET    /api/companies              - List all companies
GET    /api/companies/{id}         - Get company by ID
GET    /api/companies/ticker/{ticker} - Get company by ticker
POST   /api/companies              - Create company
DELETE /api/companies/{id}         - Delete company
```

### Financials
```
GET    /api/financials/company/{companyId} - Get financials for a company
```

### AI (Python Service - coming Week 5-6)
```
POST   /api/ai/predict   - ML growth classification
POST   /api/ai/query     - Natural language to SQL
GET    /api/ai/health    - Python service health check
```

---

## API Communication

### ML Prediction
```
POST /api/ai/predict
{ "companyId": 105 }

→ {
    "classification": "High Growth",
    "confidence": 0.93,
    "topFeatures": ["Revenue Growth", "ROE", "Low Debt"]
  }
```

### LLM SQL Generation
```
POST /api/ai/query
{ "query": "Find profitable semiconductor companies with growing revenue" }

→ {
    "sql": "SELECT ...",
    "tables_used": ["companies", "financials"]
  }
```

> AI-generated SQL is never executed directly. ASP.NET validates it against a whitelist (SELECT only, known tables, no DROP/DELETE/UPDATE/EXEC).

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

## Development Timeline

| Week | Focus | Status | Deliverable |
|------|-------|--------|-------------|
| 1-2 | DB Design | 🔜 Next | ER diagram, full schema, seed data |
| 3-4 | ASP.NET Core | ✅ Done | CRUD APIs, EF Core, Scalar docs |
| 5-6 | Python ETL + FastAPI | 🔜 | Nightly pipeline, `/health`, `/predict` |
| 7-8 | React Dashboard | 🔜 | Charts, search, company pages |
| 9-10 | ML Model | 🔜 | Random Forest classifier, `.pkl` model |
| 11 | LLM SQL + Validation | 🔜 | Chat UI, SQL guard middleware |
| 12 | Polish | 🔜 | Docker Compose, docs, demo prep |

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
- **Software Engineering** - ASP.NET Core 9
- **Database Engineering** - PostgreSQL 16
- **Data Engineering / AI** - Python + ML + LLM

---

## Author

**Sarthak Agrawal** - Computer Science Student
[GitHub](https://github.com/SarthakAgrawal442)
