using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class LateCharge_add_on_LianInstalment_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LateCharge",
                table: "Loan");

            migrationBuilder.AddColumn<decimal>(
                name: "LateCharge",
                table: "LoanInstalment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LateCharge",
                table: "LoanInstalment");

            migrationBuilder.AddColumn<decimal>(
                name: "LateCharge",
                table: "Loan",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
