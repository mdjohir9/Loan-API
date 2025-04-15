using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class loan_id_remove_from_Transction_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint first
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Loan_LoanID",
                table: "Transaction");

            // Drop the index on LoanID
            migrationBuilder.DropIndex(
                name: "IX_Transaction_LoanID",
                table: "Transaction");

            // Now drop the column
            migrationBuilder.DropColumn(
                name: "LoanID",
                table: "Transaction");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add the column
            migrationBuilder.AddColumn<int>(
                name: "LoanID",
                table: "Transaction",
                type: "int",
                nullable: true);

            // Re-create the index
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_LoanID",
                table: "Transaction",
                column: "LoanID");

            // Re-add the foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Loan_LoanID",
                table: "Transaction",
                column: "LoanID",
                principalTable: "Loan",
                principalColumn: "LoanID",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
