using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanType");

            migrationBuilder.CreateTable(
                name: "AccountBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNo = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountBalance_CustommerPersonnelInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanPlan",
                columns: table => new
                {
                    PlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MinAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinRepaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    MaxRepaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    ProcessingFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LatePaymentPenalty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descraption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPlan", x => x.PlanID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaytMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaytMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    LoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PainAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenureMonths = table.Column<int>(type: "int", nullable: false),
                    LoanStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoanStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PayMethodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.LoanID);
                    table.ForeignKey(
                        name: "FK_Loan_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_PaymentMethod_PayMethodId",
                        column: x => x.PayMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaytMethodID");
                });

            migrationBuilder.CreateTable(
                name: "LoanApplication",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    PurposeOfLoan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HasExistingLoans = table.Column<bool>(type: "bit", nullable: false),
                    ExistingLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyInstallments = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<bool>(type: "bit", maxLength: 50, nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PaytMethodID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplication", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_LoanApplication_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplication_LoanPlan_PlanID",
                        column: x => x.PlanID,
                        principalTable: "LoanPlan",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplication_PaymentMethod_PaytMethodID",
                        column: x => x.PaytMethodID,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaytMethodID");
                });

            migrationBuilder.CreateTable(
                name: "LoanInstalment",
                columns: table => new
                {
                    InstalmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    PayMethodId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInstalment", x => x.InstalmentID);
                    table.ForeignKey(
                        name: "FK_LoanInstalment_AccountBalance_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AccountBalance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanInstalment_Loan_LoanID",
                        column: x => x.LoanID,
                        principalTable: "Loan",
                        principalColumn: "LoanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanInstalment_PaymentMethod_PayMethodId",
                        column: x => x.PayMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaytMethodID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransctionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LoanID = table.Column<int>(type: "int", nullable: true),
                    PaytMethodID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransctionID);
                    table.ForeignKey(
                        name: "FK_Transaction_CustommerPersonnelInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Loan_LoanID",
                        column: x => x.LoanID,
                        principalTable: "Loan",
                        principalColumn: "LoanID");
                    table.ForeignKey(
                        name: "FK_Transaction_PaymentMethod_PaytMethodID",
                        column: x => x.PaytMethodID,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaytMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalance_AccountNo",
                table: "AccountBalance",
                column: "AccountNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalance_CustomerId",
                table: "AccountBalance",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CustomerID",
                table: "Loan",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_PayMethodId",
                table: "Loan",
                column: "PayMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_CustomerID",
                table: "LoanApplication",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_PaytMethodID",
                table: "LoanApplication",
                column: "PaytMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_PlanID",
                table: "LoanApplication",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInstalment_AccountId",
                table: "LoanInstalment",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInstalment_LoanID",
                table: "LoanInstalment",
                column: "LoanID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInstalment_PayMethodId",
                table: "LoanInstalment",
                column: "PayMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_LoanID",
                table: "Transaction",
                column: "LoanID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaytMethodID",
                table: "Transaction",
                column: "PaytMethodID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplication");

            migrationBuilder.DropTable(
                name: "LoanInstalment");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "LoanPlan");

            migrationBuilder.DropTable(
                name: "AccountBalance");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.CreateTable(
                name: "LoanType",
                columns: table => new
                {
                    LoanTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LoanTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanType", x => x.LoanTypeID);
                });
        }
    }
}
