using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class loan_application_id_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_PaymentMethod_PayMethodId",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_PayMethodId",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationID",
                schema: "dbo",
                table: "Loan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_ApplicationID",
                schema: "dbo",
                table: "Loan",
                column: "ApplicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_LoanApplication_ApplicationID",
                schema: "dbo",
                table: "Loan",
                column: "ApplicationID",
                principalSchema: "dbo",
                principalTable: "LoanApplication",
                principalColumn: "ApplicationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_LoanApplication_ApplicationID",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_ApplicationID",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "ApplicationID",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_PayMethodId",
                schema: "dbo",
                table: "Loan",
                column: "PayMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_PaymentMethod_PayMethodId",
                schema: "dbo",
                table: "Loan",
                column: "PayMethodId",
                principalSchema: "dbo",
                principalTable: "PaymentMethod",
                principalColumn: "PayMethodID");
        }
    }
}
