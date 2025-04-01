using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Loan_application_Status_type_as_byte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_PaymentMethod_PaytMethodID",
                table: "LoanApplication");

            migrationBuilder.RenameColumn(
                name: "PaytMethodID",
                table: "PaymentMethod",
                newName: "PayMethodID");

            migrationBuilder.RenameColumn(
                name: "PaytMethodID",
                table: "LoanApplication",
                newName: "PayMethodID");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplication_PaytMethodID",
                table: "LoanApplication",
                newName: "IX_LoanApplication_PayMethodID");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "LoanApplication",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplication_PaymentMethod_PayMethodID",
                table: "LoanApplication",
                column: "PayMethodID",
                principalTable: "PaymentMethod",
                principalColumn: "PayMethodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplication_PaymentMethod_PayMethodID",
                table: "LoanApplication");

            migrationBuilder.RenameColumn(
                name: "PayMethodID",
                table: "PaymentMethod",
                newName: "PaytMethodID");

            migrationBuilder.RenameColumn(
                name: "PayMethodID",
                table: "LoanApplication",
                newName: "PaytMethodID");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplication_PayMethodID",
                table: "LoanApplication",
                newName: "IX_LoanApplication_PaytMethodID");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "LoanApplication",
                type: "bit",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplication_PaymentMethod_PaytMethodID",
                table: "LoanApplication",
                column: "PaytMethodID",
                principalTable: "PaymentMethod",
                principalColumn: "PaytMethodID");
        }
    }
}
