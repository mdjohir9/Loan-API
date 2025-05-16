using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class transction_table_add_to_recharge_model_as_foregin_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_PaymentMethod_PaytMethodID",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_RechargeAccount_PaytMethodID",
                schema: "dbo",
                table: "Transaction",
                column: "PaytMethodID",
                principalSchema: "dbo",
                principalTable: "RechargeAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_RechargeAccount_PaytMethodID",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_PaymentMethod_PaytMethodID",
                schema: "dbo",
                table: "Transaction",
                column: "PaytMethodID",
                principalSchema: "dbo",
                principalTable: "PaymentMethod",
                principalColumn: "PayMethodID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
