using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class rejectAt_rejectBy_field_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessedBy",
                table: "Withdraw");

            migrationBuilder.RenameColumn(
                name: "ProcessedDate",
                table: "Withdraw",
                newName: "RejectAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveAt",
                table: "Withdraw",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApproveBy",
                table: "Withdraw",
                type: "int",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RejectBy",
                table: "Withdraw",
                type: "int",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveAt",
                table: "Withdraw");

            migrationBuilder.DropColumn(
                name: "ApproveBy",
                table: "Withdraw");

            migrationBuilder.DropColumn(
                name: "RejectBy",
                table: "Withdraw");

            migrationBuilder.RenameColumn(
                name: "RejectAt",
                table: "Withdraw",
                newName: "ProcessedDate");

            migrationBuilder.AddColumn<string>(
                name: "ProcessedBy",
                table: "Withdraw",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
