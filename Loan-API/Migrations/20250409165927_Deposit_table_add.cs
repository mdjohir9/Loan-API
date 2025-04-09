using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Deposit_table_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deposits",
                columns: table => new
                {
                    DepositID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    BankTransactCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdminRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    CustommerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposits", x => x.DepositID);
                    table.ForeignKey(
                        name: "FK_deposits_CustommerPersonnelInfo_CustommerID",
                        column: x => x.CustommerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deposits_CustommerID",
                table: "deposits",
                column: "CustommerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposits");
        }
    }
}
