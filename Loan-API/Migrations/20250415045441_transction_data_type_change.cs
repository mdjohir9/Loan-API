using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class transction_data_type_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the old column
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");

            // Add the new column with correct type
            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0); // Set appropriate default if needed
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new column
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");

            // Re-add the old column
            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""); // Set appropriate default if needed
        }


    }
}
