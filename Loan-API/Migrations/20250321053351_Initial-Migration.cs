using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustommerPersonnelInfo",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CustommerImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustommerSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationLevel = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerPersonnelInfo", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "CustommerContact",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AlternativePhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreZIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerZIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreCity = table.Column<int>(type: "int", nullable: false),
                    PerCity = table.Column<int>(type: "int", nullable: false),
                    PreState = table.Column<int>(type: "int", nullable: false),
                    PerState = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerContact", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustommerEmployment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerOrBusnName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JobTitleOrBusnType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MonthlyIncOrBusnRev = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearsOfExpOrBusnAge = table.Column<int>(type: "int", nullable: false),
                    WorkOrBusnAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerOrBusnContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerEmployment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustommerFinancialInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthlyIncomeSources = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetsOwned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liabilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerFinancialInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustommerGuarantorDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuarantorImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationshipWithApplicant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuarantorContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuarantorAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorNationalIDOrPassport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuarantorSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerGuarantorDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustommerIdentificatio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalIDOrPassport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerIdentificatio", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerIdentificatio_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustommerContact_CustomerID",
                table: "CustommerContact",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerEmployment_CustomerID",
                table: "CustommerEmployment",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerFinancialInfo_CustomerID",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerGuarantorDetails_CustomerID",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustommerIdentificatio_CustomerID",
                table: "CustommerIdentificatio",
                column: "CustomerID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustommerContact");

            migrationBuilder.DropTable(
                name: "CustommerEmployment");

            migrationBuilder.DropTable(
                name: "CustommerFinancialInfo");

            migrationBuilder.DropTable(
                name: "CustommerGuarantorDetails");

            migrationBuilder.DropTable(
                name: "CustommerIdentificatio");

            migrationBuilder.DropTable(
                name: "CustommerPersonnelInfo");
        }
    }
}
