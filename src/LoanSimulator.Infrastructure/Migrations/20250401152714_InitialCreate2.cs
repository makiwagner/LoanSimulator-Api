using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanSimulator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterest",
                table: "PaymentFlowSummary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayment",
                table: "PaymentFlowSummary",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "PaymentFlowSummary");

            migrationBuilder.DropColumn(
                name: "TotalPayment",
                table: "PaymentFlowSummary");
        }
    }
}
