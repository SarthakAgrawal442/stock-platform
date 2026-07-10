using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialBaseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // baseline - schema already created via 06_run_all.sql
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // baseline - nothing to revert
        }
    }
}