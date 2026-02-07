using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class casceding_customerInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerEmployment");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerFinancialInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerGuarantorDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerContact",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerEmployment",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerEmployment");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerFinancialInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerGuarantorDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerContact",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerEmployment",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                schema: "dbo",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                principalSchema: "dbo",
                principalTable: "CustommerPersonnelInfo",
                principalColumn: "CustomerID");
        }
    }
}
