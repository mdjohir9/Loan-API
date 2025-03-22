using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class CusommerId_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                table: "CustommerContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                table: "CustommerEmployment");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                table: "CustommerFinancialInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                table: "CustommerGuarantorDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustommerGuarantorDetails_CustomerID",
                table: "CustommerGuarantorDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustommerFinancialInfo_CustomerID",
                table: "CustommerFinancialInfo");

            migrationBuilder.DropIndex(
                name: "IX_CustommerEmployment_CustomerID",
                table: "CustommerEmployment");

            migrationBuilder.DropIndex(
                name: "IX_CustommerContact_CustomerID",
                table: "CustommerContact");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerGuarantorDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerFinancialInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "YearsOfExpOrBusnAge",
                table: "CustommerEmployment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyIncOrBusnRev",
                table: "CustommerEmployment",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerEmployment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PreState",
                table: "CustommerContact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PreCity",
                table: "CustommerContact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PerState",
                table: "CustommerContact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PerCity",
                table: "CustommerContact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerContact",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerGuarantorDetails_CustomerID",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerFinancialInfo_CustomerID",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerEmployment_CustomerID",
                table: "CustommerEmployment",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerContact_CustomerID",
                table: "CustommerContact",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                table: "CustommerContact",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                table: "CustommerEmployment",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                table: "CustommerContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                table: "CustommerEmployment");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                table: "CustommerFinancialInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                table: "CustommerGuarantorDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustommerGuarantorDetails_CustomerID",
                table: "CustommerGuarantorDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustommerFinancialInfo_CustomerID",
                table: "CustommerFinancialInfo");

            migrationBuilder.DropIndex(
                name: "IX_CustommerEmployment_CustomerID",
                table: "CustommerEmployment");

            migrationBuilder.DropIndex(
                name: "IX_CustommerContact_CustomerID",
                table: "CustommerContact");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerGuarantorDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerFinancialInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "YearsOfExpOrBusnAge",
                table: "CustommerEmployment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyIncOrBusnRev",
                table: "CustommerEmployment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerEmployment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreState",
                table: "CustommerContact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PreCity",
                table: "CustommerContact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerState",
                table: "CustommerContact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerCity",
                table: "CustommerContact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "CustommerContact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerGuarantorDetails_CustomerID",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerFinancialInfo_CustomerID",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerEmployment_CustomerID",
                table: "CustommerEmployment",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerContact_CustomerID",
                table: "CustommerContact",
                column: "CustomerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                table: "CustommerContact",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                table: "CustommerEmployment",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
