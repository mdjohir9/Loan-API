using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class loan_application_id_not_null_on_loan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_LoanApplication_ApplicationID",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationID",
                schema: "dbo",
                table: "Loan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_LoanApplication_ApplicationID",
                schema: "dbo",
                table: "Loan",
                column: "ApplicationID",
                principalSchema: "dbo",
                principalTable: "LoanApplication",
                principalColumn: "ApplicationID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_LoanApplication_ApplicationID",
                schema: "dbo",
                table: "Loan");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationID",
                schema: "dbo",
                table: "Loan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_LoanApplication_ApplicationID",
                schema: "dbo",
                table: "Loan",
                column: "ApplicationID",
                principalSchema: "dbo",
                principalTable: "LoanApplication",
                principalColumn: "ApplicationID");
        }
    }
}
