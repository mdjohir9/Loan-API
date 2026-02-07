using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Transcttion_payment_method_id_forern_key_remove_on_recharge_account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_RechargeAccount_PaytMethodID",
                schema: "dbo",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PaytMethodID",
                schema: "dbo",
                table: "Transaction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaytMethodID",
                schema: "dbo",
                table: "Transaction",
                column: "PaytMethodID");

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
    }
}
