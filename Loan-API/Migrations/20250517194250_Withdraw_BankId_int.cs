using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Withdraw_BankId_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BankId",
                schema: "dbo",
                table: "Withdraw",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BankId",
                schema: "dbo",
                table: "Withdraw",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
