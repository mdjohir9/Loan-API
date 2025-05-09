IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[CustommerPersonnelInfo] (
        [CustomerID] int NOT NULL IDENTITY,
        [CustCardNo] nvarchar(max) NOT NULL,
        [CompanyId] int NOT NULL,
        [CustommerImage] nvarchar(max) NULL,
        [CustommerSignature] nvarchar(max) NULL,
        [FullName] nvarchar(200) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [DateOfBirth] date NOT NULL,
        [Nationality] nvarchar(100) NOT NULL,
        [MaritalStatus] nvarchar(max) NULL,
        [EducationLevel] nvarchar(max) NULL,
        [Occupation] nvarchar(100) NULL,
        [NationalIDOrPassport] nvarchar(50) NULL,
        [TaxIdentificationNumber] nvarchar(max) NULL,
        [DrivingLicenseNumber] nvarchar(50) NULL,
        [IsActive] bit NULL,
        [CreatedAt] datetime2 NULL,
        [CreatedBy] int NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] int NULL,
        [DeletedAt] datetime2 NULL,
        [DeletedBy] int NULL,
        [IsDeleted] bit NULL,
        CONSTRAINT [PK_CustommerPersonnelInfo] PRIMARY KEY ([CustomerID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[HrdCompanyInfo] (
        [Id] smallint NOT NULL IDENTITY,
        [CompanyId] nvarchar(max) NOT NULL,
        [CompanyType] bit NULL,
        [HeadOfficeId] nvarchar(max) NULL,
        [CompanyName] nvarchar(max) NULL,
        [CompanyNameBangla] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [AddressBangla] nvarchar(max) NULL,
        [Country] nvarchar(max) NULL,
        [Telephone] nvarchar(max) NULL,
        [Fax] nvarchar(max) NULL,
        [DefaultCurrency] nvarchar(max) NULL,
        [BusinessType] smallint NULL,
        [MultipleBranch] bit NULL,
        [Comments] nvarchar(max) NULL,
        [CompanyLogo] nvarchar(max) NULL,
        [StartCardNo] nvarchar(max) NULL,
        [Weekend] nvarchar(max) NULL,
        [ShortName] nvarchar(max) NULL,
        [CardNoType] bit NULL,
        [FlatCode] smallint NULL,
        [CardNoDigits] smallint NULL,
        [AttMachineName] nvarchar(max) NULL,
        [PfcountDate] date NULL,
        [IsLeaveAuthority] bit NULL,
        [IsOdauthority] bit NULL,
        [Status] tinyint NULL,
        [Email] nvarchar(max) NULL,
        CONSTRAINT [PK_HrdCompanyInfo] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[LoanPlan] (
        [PlanID] int NOT NULL IDENTITY,
        [PlanName] nvarchar(150) NOT NULL,
        [MinAmount] decimal(18,2) NOT NULL,
        [MaxAmount] decimal(18,2) NOT NULL,
        [InterestRate] decimal(18,2) NOT NULL,
        [MinRepaymentPeriod] int NOT NULL,
        [MaxRepaymentPeriod] int NOT NULL,
        [ProcessingFee] decimal(18,2) NOT NULL,
        [LatePaymentPenalty] decimal(18,2) NOT NULL,
        [Descraption] nvarchar(max) NULL,
        [IsActive] tinyint NOT NULL,
        [CreatedAt] datetime2 NULL,
        [CreatedBy] int NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] int NULL,
        [Deleted] bit NULL,
        [DeletedAt] datetime2 NULL,
        [DeletedBy] int NULL,
        CONSTRAINT [PK_LoanPlan] PRIMARY KEY ([PlanID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[PaymentMethod] (
        [PayMethodID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_PaymentMethod] PRIMARY KEY ([PayMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[RechargePaymentMethod] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_RechargePaymentMethod] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[TblCountry] (
        [CountryID] int NOT NULL IDENTITY,
        [CountryName] nvarchar(max) NULL,
        [TwoCharCountryCode] nvarchar(max) NULL,
        [ThreeCharCountryCode] nvarchar(max) NULL,
        CONSTRAINT [PK_TblCountry] PRIMARY KEY ([CountryID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[TransactionType] (
        [TransactionTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Code] nvarchar(20) NOT NULL,
        [Category] nvarchar(50) NULL,
        [IsCredit] bit NOT NULL,
        [IsActive] bit NOT NULL,
        [Description] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [CreatedBy] nvarchar(50) NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] nvarchar(50) NULL,
        CONSTRAINT [PK_TransactionType] PRIMARY KEY ([TransactionTypeID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[UserRole] (
        [UserRoleId] int NOT NULL IDENTITY,
        [CompanyId] nvarchar(max) NULL,
        [UserRoleName] nvarchar(100) NOT NULL,
        [Permissions] nvarchar(max) NOT NULL,
        [DataAccessLevel] int NULL,
        [Ordering] int NOT NULL,
        [IsActive] bit NULL,
        [Deleted] bit NULL,
        [CreatedAt] datetime2 NULL,
        [CreatedBy] int NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] int NULL,
        [DeletedAt] datetime2 NULL,
        [DeletedBy] int NULL,
        CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserRoleId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserId] int NOT NULL IDENTITY,
        [CompanyId] nvarchar(max) NULL,
        [FirstName] nvarchar(100) NULL,
        [LastName] nvarchar(100) NULL,
        [UserName] nvarchar(100) NOT NULL,
        [UserImage] nvarchar(max) NULL,
        [UserPassword] nvarchar(150) NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [UserRoleID] int NOT NULL,
        [IsGuestUser] bit NULL,
        [IsApprovingAuthority] bit NULL,
        [ReferenceID] varchar(50) NULL,
        [AdditionalPermissions] nvarchar(max) NULL,
        [RemovedPermissions] nvarchar(max) NULL,
        [DataAccessPermission] nvarchar(max) NULL,
        [IsActive] bit NULL,
        [CreatedAt] datetime2 NULL,
        [CreatedBy] int NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] int NULL,
        [Deleted] bit NULL,
        [DeletedAt] datetime2 NULL,
        [DeletedBy] int NULL,
        [IsAdministrator] bit NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[AccountBalance] (
        [Id] int NOT NULL IDENTITY,
        [AccountNo] int NOT NULL,
        [CustomerId] int NOT NULL,
        [BalanceAmount] decimal(18,2) NOT NULL,
        [IsActive] tinyint NOT NULL,
        [CreatedAt] datetime2 NULL,
        [CreatedBy] int NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] int NULL,
        [Deleted] bit NULL,
        [DeletedAt] datetime2 NULL,
        [DeletedBy] int NULL,
        CONSTRAINT [PK_AccountBalance] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AccountBalance_CustommerPersonnelInfo_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[CustommerContact] (
        [ID] int NOT NULL IDENTITY,
        [PhoneNumber] nvarchar(20) NULL,
        [AlternativePhoneNumber] nvarchar(20) NULL,
        [EmailAddress] nvarchar(max) NULL,
        [PreStreet] nvarchar(max) NULL,
        [PerStreet] nvarchar(max) NULL,
        [PreZIP] nvarchar(max) NULL,
        [PerZIP] nvarchar(max) NULL,
        [PreCity] nvarchar(max) NULL,
        [PerCity] nvarchar(max) NULL,
        [PreState] nvarchar(max) NULL,
        [PerState] nvarchar(max) NULL,
        [CustomerID] int NULL,
        CONSTRAINT [PK_CustommerContact] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerContact_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[CustommerEmployment] (
        [ID] int NOT NULL IDENTITY,
        [EmploymentType] nvarchar(max) NULL,
        [EmployerOrBusnName] nvarchar(200) NULL,
        [JobTitleOrBusnType] nvarchar(100) NULL,
        [MonthlyIncOrBusnRev] decimal(18,2) NULL,
        [YearsOfExpOrBusnAge] int NULL,
        [WorkOrBusnAddress] nvarchar(max) NULL,
        [EmployerOrBusnContact] nvarchar(max) NULL,
        [CustomerID] int NULL,
        CONSTRAINT [PK_CustommerEmployment] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[CustommerFinancialInfo] (
        [ID] int NOT NULL IDENTITY,
        [BankName] nvarchar(max) NULL,
        [AccountNumber] nvarchar(max) NULL,
        [MonthlyIncomeSources] decimal(18,2) NOT NULL,
        [MonthlyExpenses] decimal(18,2) NOT NULL,
        [AssetsOwned] nvarchar(max) NULL,
        [Liabilities] nvarchar(max) NULL,
        [CustomerID] int NULL,
        CONSTRAINT [PK_CustommerFinancialInfo] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[CustommerGuarantorDetails] (
        [ID] int NOT NULL IDENTITY,
        [GuarantorImage] nvarchar(max) NULL,
        [GuarantorFullName] nvarchar(max) NULL,
        [RelationshipWithApplicant] nvarchar(max) NULL,
        [GuarantorContactNumber] nvarchar(max) NULL,
        [GuarantorAddress] nvarchar(max) NULL,
        [GuarantorNationalIDOrPassport] nvarchar(max) NULL,
        [GuarantorSignature] nvarchar(max) NULL,
        [CustomerID] int NULL,
        CONSTRAINT [PK_CustommerGuarantorDetails] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[Recharge] (
        [RechargeID] int NOT NULL IDENTITY,
        [BankAccountNumber] nvarchar(20) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [RequestedDate] datetime2 NOT NULL,
        [IsApproved] bit NULL,
        [BankTransactCode] nvarchar(100) NULL,
        [AdminRemarks] nvarchar(500) NULL,
        [Statement] nvarchar(max) NULL,
        [ApproveAt] datetime2 NULL,
        [ApproveBy] int NULL,
        [PaymentMethodID] int NOT NULL,
        [BankId] int NOT NULL,
        [CustommerID] int NOT NULL,
        CONSTRAINT [PK_Recharge] PRIMARY KEY ([RechargeID]),
        CONSTRAINT [FK_Recharge_CustommerPersonnelInfo_CustommerID] FOREIGN KEY ([CustommerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[Withdraw] (
        [WithdrawaID] int NOT NULL IDENTITY,
        [PaymentMethodID] int NOT NULL,
        [BankName] nvarchar(max) NOT NULL,
        [AccountNumber] nvarchar(20) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [RequestedDate] datetime2 NOT NULL,
        [IsApproved] bit NULL,
        [TransactionCode] nvarchar(100) NULL,
        [AdminRemarks] nvarchar(500) NULL,
        [ApproveAt] datetime2 NULL,
        [ApproveBy] int NULL,
        [RejectAt] datetime2 NULL,
        [RejectBy] int NULL,
        [CustommerID] int NOT NULL,
        CONSTRAINT [PK_Withdraw] PRIMARY KEY ([WithdrawaID]),
        CONSTRAINT [FK_Withdraw_CustommerPersonnelInfo_CustommerID] FOREIGN KEY ([CustommerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[Loan] (
        [LoanID] int NOT NULL IDENTITY,
        [LoanNumber] nvarchar(max) NOT NULL,
        [LoanAmount] decimal(18,2) NOT NULL,
        [DepositAmount] decimal(18,2) NULL,
        [PaidAmount] decimal(18,2) NOT NULL,
        [DueAmount] decimal(18,2) NOT NULL,
        [TotalPayableAmount] decimal(18,2) NULL,
        [TotalInterest] decimal(18,2) NULL,
        [MonthlyInstallment] decimal(18,2) NULL,
        [TenureMonths] int NOT NULL,
        [LoanStartDate] datetime2 NOT NULL,
        [LoanEndDate] datetime2 NULL,
        [LoanStatus] tinyint NOT NULL,
        [Purpose] nvarchar(max) NULL,
        [DisbursementDate] datetime2 NULL,
        [CustomerID] int NOT NULL,
        [PayMethodId] int NULL,
        [PlanID] int NULL,
        CONSTRAINT [PK_Loan] PRIMARY KEY ([LoanID]),
        CONSTRAINT [FK_Loan_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Loan_LoanPlan_PlanID] FOREIGN KEY ([PlanID]) REFERENCES [dbo].[LoanPlan] ([PlanID]),
        CONSTRAINT [FK_Loan_PaymentMethod_PayMethodId] FOREIGN KEY ([PayMethodId]) REFERENCES [dbo].[PaymentMethod] ([PayMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[LoanApplication] (
        [ApplicationID] int NOT NULL IDENTITY,
        [LoanAmount] decimal(18,2) NOT NULL,
        [DepositAmount] decimal(18,2) NULL,
        [LateCharge] decimal(18,2) NULL,
        [RepaymentPeriod] int NOT NULL,
        [PurposeOfLoan] nvarchar(255) NOT NULL,
        [HasExistingLoans] bit NOT NULL,
        [ExistingLoanAmount] decimal(18,2) NULL,
        [LenderName] nvarchar(max) NULL,
        [MonthlyInstallments] decimal(18,2) NULL,
        [Status] tinyint NOT NULL,
        [ApplicationDate] datetime2 NOT NULL,
        [ApplyedAt] datetime2 NULL,
        [ApplyedBy] int NULL,
        [ApprovedAt] datetime2 NULL,
        [ApprovedBy] int NULL,
        [RejectAt] datetime2 NULL,
        [RejectedBy] int NULL,
        [UpdatedAt] datetime2 NULL,
        [UpdatedBy] int NULL,
        [PlanID] int NOT NULL,
        [CustomerID] int NOT NULL,
        [PayMethodID] int NULL,
        CONSTRAINT [PK_LoanApplication] PRIMARY KEY ([ApplicationID]),
        CONSTRAINT [FK_LoanApplication_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanApplication_LoanPlan_PlanID] FOREIGN KEY ([PlanID]) REFERENCES [dbo].[LoanPlan] ([PlanID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanApplication_PaymentMethod_PayMethodID] FOREIGN KEY ([PayMethodID]) REFERENCES [dbo].[PaymentMethod] ([PayMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[Transaction] (
        [TransctionID] int NOT NULL IDENTITY,
        [TransactionType] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [CustomerId] int NOT NULL,
        [PaytMethodID] int NOT NULL,
        [Remarks] nvarchar(max) NULL,
        CONSTRAINT [PK_Transaction] PRIMARY KEY ([TransctionID]),
        CONSTRAINT [FK_Transaction_CustommerPersonnelInfo_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Transaction_PaymentMethod_PaytMethodID] FOREIGN KEY ([PaytMethodID]) REFERENCES [dbo].[PaymentMethod] ([PayMethodID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[RechargeAccount] (
        [Id] int NOT NULL IDENTITY,
        [RecPaymentMethodId] int NOT NULL,
        [BankOrWalletName] nvarchar(max) NOT NULL,
        [AccountName] nvarchar(max) NOT NULL,
        [AccountNumber] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_RechargeAccount] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RechargeAccount_RechargePaymentMethod_RecPaymentMethodId] FOREIGN KEY ([RecPaymentMethodId]) REFERENCES [dbo].[RechargePaymentMethod] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE TABLE [dbo].[LoanInstalment] (
        [InstalmentID] int NOT NULL IDENTITY,
        [LoanID] int NOT NULL,
        [PaymentDate] date NOT NULL,
        [Status] tinyint NOT NULL,
        [PayMethodId] int NULL,
        [AccountId] int NULL,
        [AmountPaid] decimal(18,2) NOT NULL,
        [LateCharge] decimal(18,2) NOT NULL,
        [LateChargePaid] decimal(18,2) NULL,
        CONSTRAINT [PK_LoanInstalment] PRIMARY KEY ([InstalmentID]),
        CONSTRAINT [FK_LoanInstalment_AccountBalance_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[AccountBalance] ([Id]),
        CONSTRAINT [FK_LoanInstalment_Loan_LoanID] FOREIGN KEY ([LoanID]) REFERENCES [dbo].[Loan] ([LoanID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanInstalment_PaymentMethod_PayMethodId] FOREIGN KEY ([PayMethodId]) REFERENCES [dbo].[PaymentMethod] ([PayMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE UNIQUE INDEX [IX_AccountBalance_AccountNo] ON [dbo].[AccountBalance] ([AccountNo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_AccountBalance_CustomerId] ON [dbo].[AccountBalance] ([CustomerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerContact_CustomerID] ON [dbo].[CustommerContact] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerEmployment_CustomerID] ON [dbo].[CustommerEmployment] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerFinancialInfo_CustomerID] ON [dbo].[CustommerFinancialInfo] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerGuarantorDetails_CustomerID] ON [dbo].[CustommerGuarantorDetails] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Loan_CustomerID] ON [dbo].[Loan] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Loan_PayMethodId] ON [dbo].[Loan] ([PayMethodId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Loan_PlanID] ON [dbo].[Loan] ([PlanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_LoanApplication_CustomerID] ON [dbo].[LoanApplication] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_LoanApplication_PayMethodID] ON [dbo].[LoanApplication] ([PayMethodID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_LoanApplication_PlanID] ON [dbo].[LoanApplication] ([PlanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_LoanInstalment_AccountId] ON [dbo].[LoanInstalment] ([AccountId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_LoanInstalment_LoanID] ON [dbo].[LoanInstalment] ([LoanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_LoanInstalment_PayMethodId] ON [dbo].[LoanInstalment] ([PayMethodId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Recharge_CustommerID] ON [dbo].[Recharge] ([CustommerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_RechargeAccount_RecPaymentMethodId] ON [dbo].[RechargeAccount] ([RecPaymentMethodId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Transaction_CustomerId] ON [dbo].[Transaction] ([CustomerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Transaction_PaytMethodID] ON [dbo].[Transaction] ([PaytMethodID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    CREATE INDEX [IX_Withdraw_CustommerID] ON [dbo].[Withdraw] ([CustommerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250508111810_initial_db'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250508111810_initial_db', N'9.0.3');
END;

COMMIT;
GO

