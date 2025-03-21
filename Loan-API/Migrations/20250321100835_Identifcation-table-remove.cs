using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Identifcationtableremove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrivingLicenseNumber",
                table: "CustommerPersonnelInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIDOrPassport",
                table: "CustommerPersonnelInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxIdentificationNumber",
                table: "CustommerPersonnelInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrivingLicenseNumber",
                table: "CustommerPersonnelInfo");

            migrationBuilder.DropColumn(
                name: "NationalIDOrPassport",
                table: "CustommerPersonnelInfo");

            migrationBuilder.DropColumn(
                name: "TaxIdentificationNumber",
                table: "CustommerPersonnelInfo");
        }
    }
}
