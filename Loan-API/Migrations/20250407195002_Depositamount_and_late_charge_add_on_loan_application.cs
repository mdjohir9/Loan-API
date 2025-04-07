using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Depositamount_and_late_charge_add_on_loan_application : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DepositAmount",
                table: "LoanApplication",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LateCharge",
                table: "LoanApplication",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DepositAmount",
                table: "Loan",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LateCharge",
                table: "Loan",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "LateCharge",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "LateCharge",
                table: "Loan");
        }
    }
}
