using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class Approveat_misteck_correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApprovedAt",
                schema: "dbo",
                table: "Withdraw",
                newName: "ApplyedAt");

            migrationBuilder.RenameColumn(
                name: "ApprovedAt",
                schema: "dbo",
                table: "Recharge",
                newName: "ApplyedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplyedAt",
                schema: "dbo",
                table: "Withdraw",
                newName: "ApprovedAt");

            migrationBuilder.RenameColumn(
                name: "ApplyedAt",
                schema: "dbo",
                table: "Recharge",
                newName: "ApprovedAt");
        }
    }
}
