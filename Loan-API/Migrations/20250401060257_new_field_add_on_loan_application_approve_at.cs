using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class new_field_add_on_loan_application_approve_at : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApplyedAt",
                table: "LoanApplication",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplyedBy",
                table: "LoanApplication",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "LoanApplication",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "LoanApplication",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectAt",
                table: "LoanApplication",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RejectedBy",
                table: "LoanApplication",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "LoanApplication",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "LoanApplication",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyedAt",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "ApplyedBy",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "RejectAt",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "LoanApplication");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LoanApplication");
        }
    }
}
