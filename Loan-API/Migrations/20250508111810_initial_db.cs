using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_API.Migrations
{
    /// <inheritdoc />
    public partial class initial_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "CustommerPersonnelInfo",
                schema: "dbo",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CustommerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustommerSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NationalIDOrPassport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrivingLicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                name: "HrdCompanyInfo",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyType = table.Column<bool>(type: "bit", nullable: true),
                    HeadOfficeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameBangla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressBangla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessType = table.Column<short>(type: "smallint", nullable: true),
                    MultipleBranch = table.Column<bool>(type: "bit", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weekend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNoType = table.Column<bool>(type: "bit", nullable: true),
                    FlatCode = table.Column<short>(type: "smallint", nullable: true),
                    CardNoDigits = table.Column<short>(type: "smallint", nullable: true),
                    AttMachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PfcountDate = table.Column<DateOnly>(type: "date", nullable: true),
                    IsLeaveAuthority = table.Column<bool>(type: "bit", nullable: true),
                    IsOdauthority = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrdCompanyInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanPlan",
                schema: "dbo",
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
                schema: "dbo",
                columns: table => new
                {
                    PayMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PayMethodID);
                });

            migrationBuilder.CreateTable(
                name: "RechargePaymentMethod",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargePaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblCountry",
                schema: "dbo",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoCharCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreeCharCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCountry", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                schema: "dbo",
                columns: table => new
                {
                    TransactionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsCredit = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "dbo",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAccessLevel = table.Column<int>(type: "int", nullable: true),
                    Ordering = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(150)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserRoleID = table.Column<int>(type: "int", nullable: false),
                    IsGuestUser = table.Column<bool>(type: "bit", nullable: true),
                    IsApprovingAuthority = table.Column<bool>(type: "bit", nullable: true),
                    ReferenceID = table.Column<string>(type: "varchar(50)", nullable: true),
                    AdditionalPermissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemovedPermissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAccessPermission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    IsAdministrator = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AccountBalance",
                schema: "dbo",
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
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustommerContact",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AlternativePhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreZIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerZIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerContact", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerContact_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "CustommerEmployment",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerOrBusnName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    JobTitleOrBusnType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MonthlyIncOrBusnRev = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    YearsOfExpOrBusnAge = table.Column<int>(type: "int", nullable: true),
                    WorkOrBusnAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerOrBusnContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerEmployment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "CustommerFinancialInfo",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyIncomeSources = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetsOwned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liabilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerFinancialInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "CustommerGuarantorDetails",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuarantorImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipWithApplicant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorNationalIDOrPassport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuarantorSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustommerGuarantorDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "Recharge",
                schema: "dbo",
                columns: table => new
                {
                    RechargeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    BankTransactCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdminRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Statement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproveBy = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    CustommerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharge", x => x.RechargeID);
                    table.ForeignKey(
                        name: "FK_Recharge_CustommerPersonnelInfo_CustommerID",
                        column: x => x.CustommerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Withdraw",
                schema: "dbo",
                columns: table => new
                {
                    WithdrawaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    TransactionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdminRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApproveAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproveBy = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    RejectAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectBy = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    CustommerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraw", x => x.WithdrawaID);
                    table.ForeignKey(
                        name: "FK_Withdraw_CustommerPersonnelInfo_CustommerID",
                        column: x => x.CustommerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                schema: "dbo",
                columns: table => new
                {
                    LoanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPayableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MonthlyInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TenureMonths = table.Column<int>(type: "int", nullable: false),
                    LoanStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoanStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisbursementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PayMethodId = table.Column<int>(type: "int", nullable: true),
                    PlanID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.LoanID);
                    table.ForeignKey(
                        name: "FK_Loan_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_LoanPlan_PlanID",
                        column: x => x.PlanID,
                        principalSchema: "dbo",
                        principalTable: "LoanPlan",
                        principalColumn: "PlanID");
                    table.ForeignKey(
                        name: "FK_Loan_PaymentMethod_PayMethodId",
                        column: x => x.PayMethodId,
                        principalSchema: "dbo",
                        principalTable: "PaymentMethod",
                        principalColumn: "PayMethodID");
                });

            migrationBuilder.CreateTable(
                name: "LoanApplication",
                schema: "dbo",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LateCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RepaymentPeriod = table.Column<int>(type: "int", nullable: false),
                    PurposeOfLoan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HasExistingLoans = table.Column<bool>(type: "bit", nullable: false),
                    ExistingLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyInstallments = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplyedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplyedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    RejectAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    PlanID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PayMethodID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplication", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_LoanApplication_CustommerPersonnelInfo_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplication_LoanPlan_PlanID",
                        column: x => x.PlanID,
                        principalSchema: "dbo",
                        principalTable: "LoanPlan",
                        principalColumn: "PlanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplication_PaymentMethod_PayMethodID",
                        column: x => x.PayMethodID,
                        principalSchema: "dbo",
                        principalTable: "PaymentMethod",
                        principalColumn: "PayMethodID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "dbo",
                columns: table => new
                {
                    TransctionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PaytMethodID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransctionID);
                    table.ForeignKey(
                        name: "FK_Transaction_CustommerPersonnelInfo_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "CustommerPersonnelInfo",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_PaymentMethod_PaytMethodID",
                        column: x => x.PaytMethodID,
                        principalSchema: "dbo",
                        principalTable: "PaymentMethod",
                        principalColumn: "PayMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RechargeAccount",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecPaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    BankOrWalletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RechargeAccount_RechargePaymentMethod_RecPaymentMethodId",
                        column: x => x.RecPaymentMethodId,
                        principalSchema: "dbo",
                        principalTable: "RechargePaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanInstalment",
                schema: "dbo",
                columns: table => new
                {
                    InstalmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    PayMethodId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LateCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LateChargePaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanInstalment", x => x.InstalmentID);
                    table.ForeignKey(
                        name: "FK_LoanInstalment_AccountBalance_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "dbo",
                        principalTable: "AccountBalance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoanInstalment_Loan_LoanID",
                        column: x => x.LoanID,
                        principalSchema: "dbo",
                        principalTable: "Loan",
                        principalColumn: "LoanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanInstalment_PaymentMethod_PayMethodId",
                        column: x => x.PayMethodId,
                        principalSchema: "dbo",
                        principalTable: "PaymentMethod",
                        principalColumn: "PayMethodID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalance_AccountNo",
                schema: "dbo",
                table: "AccountBalance",
                column: "AccountNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountBalance_CustomerId",
                schema: "dbo",
                table: "AccountBalance",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerContact_CustomerID",
                schema: "dbo",
                table: "CustommerContact",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerEmployment_CustomerID",
                schema: "dbo",
                table: "CustommerEmployment",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerFinancialInfo_CustomerID",
                schema: "dbo",
                table: "CustommerFinancialInfo",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustommerGuarantorDetails_CustomerID",
                schema: "dbo",
                table: "CustommerGuarantorDetails",
                column: "CustomerID",
                unique: true,
                filter: "[CustomerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CustomerID",
                schema: "dbo",
                table: "Loan",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_PayMethodId",
                schema: "dbo",
                table: "Loan",
                column: "PayMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_PlanID",
                schema: "dbo",
                table: "Loan",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_CustomerID",
                schema: "dbo",
                table: "LoanApplication",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_PayMethodID",
                schema: "dbo",
                table: "LoanApplication",
                column: "PayMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_PlanID",
                schema: "dbo",
                table: "LoanApplication",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInstalment_AccountId",
                schema: "dbo",
                table: "LoanInstalment",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInstalment_LoanID",
                schema: "dbo",
                table: "LoanInstalment",
                column: "LoanID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanInstalment_PayMethodId",
                schema: "dbo",
                table: "LoanInstalment",
                column: "PayMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Recharge_CustommerID",
                schema: "dbo",
                table: "Recharge",
                column: "CustommerID");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeAccount_RecPaymentMethodId",
                schema: "dbo",
                table: "RechargeAccount",
                column: "RecPaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                schema: "dbo",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PaytMethodID",
                schema: "dbo",
                table: "Transaction",
                column: "PaytMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Withdraw_CustommerID",
                schema: "dbo",
                table: "Withdraw",
                column: "CustommerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustommerContact",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustommerEmployment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustommerFinancialInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustommerGuarantorDetails",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HrdCompanyInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoanApplication",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoanInstalment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Recharge",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RechargeAccount",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TblCountry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransactionType",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Withdraw",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AccountBalance",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Loan",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RechargePaymentMethod",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustommerPersonnelInfo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "LoanPlan",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PaymentMethod",
                schema: "dbo");
        }
    }
}
