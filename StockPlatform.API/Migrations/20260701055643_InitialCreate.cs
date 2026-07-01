using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StockPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    sector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    industry = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    market_cap = table.Column<decimal>(type: "numeric", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "financials",
                columns: table => new
                {
                    financial_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    report_date = table.Column<DateOnly>(type: "date", nullable: false),
                    revenue = table.Column<decimal>(type: "numeric", nullable: true),
                    net_income = table.Column<decimal>(type: "numeric", nullable: true),
                    eps = table.Column<decimal>(type: "numeric", nullable: true),
                    roe = table.Column<decimal>(type: "numeric", nullable: true),
                    debt_to_equity = table.Column<decimal>(type: "numeric", nullable: true),
                    revenue_growth = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financials", x => x.financial_id);
                    table.ForeignKey(
                        name: "FK_financials_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_sector_market_cap",
                table: "companies",
                columns: new[] { "sector", "market_cap" });

            migrationBuilder.CreateIndex(
                name: "IX_companies_ticker",
                table: "companies",
                column: "ticker",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_financials_company_id_report_date",
                table: "financials",
                columns: new[] { "company_id", "report_date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financials");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
