using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class recharge_payment_method_reacharge_account_table_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deposits_CustommerPersonnelInfo_CustommerID",
                table: "deposits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deposits",
                table: "deposits");

            migrationBuilder.RenameTable(
                name: "deposits",
                newName: "Deposits");

            migrationBuilder.RenameIndex(
                name: "IX_deposits_CustommerID",
                table: "Deposits",
                newName: "IX_Deposits_CustommerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits",
                column: "DepositID");

            migrationBuilder.CreateTable(
                name: "RechargePaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargePaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RechargeAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecPaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    BankOrWalletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RechargeAccount_RechargePaymentMethod_RecPaymentMethodId",
                        column: x => x.RecPaymentMethodId,
                        principalTable: "RechargePaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RechargeAccount_RecPaymentMethodId",
                table: "RechargeAccount",
                column: "RecPaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_CustommerPersonnelInfo_CustommerID",
                table: "Deposits",
                column: "CustommerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_CustommerPersonnelInfo_CustommerID",
                table: "Deposits");

            migrationBuilder.DropTable(
                name: "RechargeAccount");

            migrationBuilder.DropTable(
                name: "RechargePaymentMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits");

            migrationBuilder.RenameTable(
                name: "Deposits",
                newName: "deposits");

            migrationBuilder.RenameIndex(
                name: "IX_Deposits_CustommerID",
                table: "deposits",
                newName: "IX_deposits_CustommerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deposits",
                table: "deposits",
                column: "DepositID");

            migrationBuilder.AddForeignKey(
                name: "FK_deposits_CustommerPersonnelInfo_CustommerID",
                table: "deposits",
                column: "CustommerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
