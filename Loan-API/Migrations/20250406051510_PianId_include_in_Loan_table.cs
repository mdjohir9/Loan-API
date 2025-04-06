using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class PianId_include_in_Loan_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanID",
                table: "Loan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_PlanID",
                table: "Loan",
                column: "PlanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_LoanPlan_PlanID",
                table: "Loan",
                column: "PlanID",
                principalTable: "LoanPlan",
                principalColumn: "PlanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_LoanPlan_PlanID",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_PlanID",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "PlanID",
                table: "Loan");
        }
    }
}
