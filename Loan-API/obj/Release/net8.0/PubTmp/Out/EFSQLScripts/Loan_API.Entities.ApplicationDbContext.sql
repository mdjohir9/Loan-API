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
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE TABLE [CustommerPersonnelInfo] (
        [CustomerID] int NOT NULL IDENTITY,
        [CustCardNo] nvarchar(max) NOT NULL,
        [CompanyId] int NOT NULL,
        [CustommerImage] nvarchar(max) NOT NULL,
        [CustommerSignature] nvarchar(max) NOT NULL,
        [FullName] nvarchar(200) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [DateOfBirth] date NOT NULL,
        [Nationality] nvarchar(100) NOT NULL,
        [MaritalStatus] nvarchar(max) NOT NULL,
        [EducationLevel] int NOT NULL,
        [Occupation] nvarchar(100) NULL,
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
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE TABLE [CustommerContact] (
        [ID] int NOT NULL IDENTITY,
        [PhoneNumber] nvarchar(20) NOT NULL,
        [AlternativePhoneNumber] nvarchar(20) NULL,
        [EmailAddress] nvarchar(max) NOT NULL,
        [PreStreet] nvarchar(max) NOT NULL,
        [PerStreet] nvarchar(max) NOT NULL,
        [PreZIP] nvarchar(max) NOT NULL,
        [PerZIP] nvarchar(max) NOT NULL,
        [PreCity] int NOT NULL,
        [PerCity] int NOT NULL,
        [PreState] int NOT NULL,
        [PerState] int NOT NULL,
        [CustomerID] int NOT NULL,
        CONSTRAINT [PK_CustommerContact] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerContact_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE TABLE [CustommerEmployment] (
        [ID] int NOT NULL IDENTITY,
        [EmploymentType] nvarchar(max) NOT NULL,
        [EmployerOrBusnName] nvarchar(200) NULL,
        [JobTitleOrBusnType] nvarchar(100) NULL,
        [MonthlyIncOrBusnRev] decimal(18,2) NOT NULL,
        [YearsOfExpOrBusnAge] int NOT NULL,
        [WorkOrBusnAddress] nvarchar(max) NULL,
        [EmployerOrBusnContact] nvarchar(max) NULL,
        [CustomerID] int NOT NULL,
        CONSTRAINT [PK_CustommerEmployment] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE TABLE [CustommerFinancialInfo] (
        [ID] int NOT NULL IDENTITY,
        [BankName] nvarchar(max) NOT NULL,
        [AccountNumber] nvarchar(max) NOT NULL,
        [MonthlyIncomeSources] decimal(18,2) NOT NULL,
        [MonthlyExpenses] decimal(18,2) NOT NULL,
        [AssetsOwned] nvarchar(max) NULL,
        [Liabilities] nvarchar(max) NULL,
        [CustomerID] int NOT NULL,
        CONSTRAINT [PK_CustommerFinancialInfo] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE TABLE [CustommerGuarantorDetails] (
        [ID] int NOT NULL IDENTITY,
        [GuarantorImage] nvarchar(max) NULL,
        [GuarantorFullName] nvarchar(max) NOT NULL,
        [RelationshipWithApplicant] nvarchar(max) NOT NULL,
        [GuarantorContactNumber] nvarchar(max) NOT NULL,
        [GuarantorAddress] nvarchar(max) NULL,
        [GuarantorNationalIDOrPassport] nvarchar(max) NOT NULL,
        [GuarantorSignature] nvarchar(max) NULL,
        [CustomerID] int NOT NULL,
        CONSTRAINT [PK_CustommerGuarantorDetails] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE TABLE [CustommerIdentificatio] (
        [ID] int NOT NULL IDENTITY,
        [NationalIDOrPassport] nvarchar(max) NOT NULL,
        [TaxIdentificationNumber] nvarchar(max) NULL,
        [DrivingLicenseNumber] nvarchar(max) NULL,
        [CustomerID] int NOT NULL,
        CONSTRAINT [PK_CustommerIdentificatio] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_CustommerIdentificatio_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustommerContact_CustomerID] ON [CustommerContact] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustommerEmployment_CustomerID] ON [CustommerEmployment] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustommerFinancialInfo_CustomerID] ON [CustommerFinancialInfo] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustommerGuarantorDetails_CustomerID] ON [CustommerGuarantorDetails] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CustommerIdentificatio_CustomerID] ON [CustommerIdentificatio] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321053351_Initial-Migration'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250321053351_Initial-Migration', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PreZIP');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PreZIP] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PreStreet');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PreStreet] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PhoneNumber');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PhoneNumber] nvarchar(20) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PerZIP');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PerZIP] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PerStreet');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PerStreet] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'EmailAddress');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [EmailAddress] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321055327_custommer_contact_field_null_able_false'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250321055327_custommer_contact_field_null_able_false', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerPersonnelInfo]') AND [c].[name] = N'MaritalStatus');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [CustommerPersonnelInfo] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [CustommerPersonnelInfo] ALTER COLUMN [MaritalStatus] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerPersonnelInfo]') AND [c].[name] = N'EducationLevel');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [CustommerPersonnelInfo] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [CustommerPersonnelInfo] ALTER COLUMN [EducationLevel] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerPersonnelInfo]') AND [c].[name] = N'CustommerSignature');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [CustommerPersonnelInfo] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [CustommerPersonnelInfo] ALTER COLUMN [CustommerSignature] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerPersonnelInfo]') AND [c].[name] = N'CustommerImage');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [CustommerPersonnelInfo] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [CustommerPersonnelInfo] ALTER COLUMN [CustommerImage] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerIdentificatio]') AND [c].[name] = N'NationalIDOrPassport');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [CustommerIdentificatio] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [CustommerIdentificatio] ALTER COLUMN [NationalIDOrPassport] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerGuarantorDetails]') AND [c].[name] = N'RelationshipWithApplicant');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [CustommerGuarantorDetails] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [CustommerGuarantorDetails] ALTER COLUMN [RelationshipWithApplicant] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerGuarantorDetails]') AND [c].[name] = N'GuarantorNationalIDOrPassport');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [CustommerGuarantorDetails] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [CustommerGuarantorDetails] ALTER COLUMN [GuarantorNationalIDOrPassport] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerGuarantorDetails]') AND [c].[name] = N'GuarantorFullName');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [CustommerGuarantorDetails] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [CustommerGuarantorDetails] ALTER COLUMN [GuarantorFullName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerGuarantorDetails]') AND [c].[name] = N'GuarantorContactNumber');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [CustommerGuarantorDetails] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [CustommerGuarantorDetails] ALTER COLUMN [GuarantorContactNumber] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerFinancialInfo]') AND [c].[name] = N'BankName');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [CustommerFinancialInfo] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [CustommerFinancialInfo] ALTER COLUMN [BankName] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerFinancialInfo]') AND [c].[name] = N'AccountNumber');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [CustommerFinancialInfo] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [CustommerFinancialInfo] ALTER COLUMN [AccountNumber] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerEmployment]') AND [c].[name] = N'EmploymentType');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [CustommerEmployment] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [CustommerEmployment] ALTER COLUMN [EmploymentType] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321060036_custommer_personnel_info_nullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250321060036_custommer_personnel_info_nullable', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321100835_Identifcation-table-remove'
)
BEGIN
    ALTER TABLE [CustommerPersonnelInfo] ADD [DrivingLicenseNumber] nvarchar(50) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321100835_Identifcation-table-remove'
)
BEGIN
    ALTER TABLE [CustommerPersonnelInfo] ADD [NationalIDOrPassport] nvarchar(50) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321100835_Identifcation-table-remove'
)
BEGIN
    ALTER TABLE [CustommerPersonnelInfo] ADD [TaxIdentificationNumber] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321100835_Identifcation-table-remove'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250321100835_Identifcation-table-remove', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321102436_identification-table-remove-final'
)
BEGIN
    DROP TABLE [CustommerIdentificatio];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250321102436_identification-table-remove-final'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250321102436_identification-table-remove-final', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerContact] DROP CONSTRAINT [FK_CustommerContact_CustommerPersonnelInfo_CustomerID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerEmployment] DROP CONSTRAINT [FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerFinancialInfo] DROP CONSTRAINT [FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerGuarantorDetails] DROP CONSTRAINT [FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DROP INDEX [IX_CustommerGuarantorDetails_CustomerID] ON [CustommerGuarantorDetails];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DROP INDEX [IX_CustommerFinancialInfo_CustomerID] ON [CustommerFinancialInfo];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DROP INDEX [IX_CustommerEmployment_CustomerID] ON [CustommerEmployment];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DROP INDEX [IX_CustommerContact_CustomerID] ON [CustommerContact];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerGuarantorDetails]') AND [c].[name] = N'CustomerID');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [CustommerGuarantorDetails] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [CustommerGuarantorDetails] ALTER COLUMN [CustomerID] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerFinancialInfo]') AND [c].[name] = N'CustomerID');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [CustommerFinancialInfo] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [CustommerFinancialInfo] ALTER COLUMN [CustomerID] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerEmployment]') AND [c].[name] = N'YearsOfExpOrBusnAge');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [CustommerEmployment] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [CustommerEmployment] ALTER COLUMN [YearsOfExpOrBusnAge] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerEmployment]') AND [c].[name] = N'MonthlyIncOrBusnRev');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [CustommerEmployment] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [CustommerEmployment] ALTER COLUMN [MonthlyIncOrBusnRev] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerEmployment]') AND [c].[name] = N'CustomerID');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [CustommerEmployment] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [CustommerEmployment] ALTER COLUMN [CustomerID] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PreState');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PreState] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PreCity');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PreCity] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PerState');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PerState] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var26 sysname;
    SELECT @var26 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'PerCity');
    IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var26 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [PerCity] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    DECLARE @var27 sysname;
    SELECT @var27 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerContact]') AND [c].[name] = N'CustomerID');
    IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [CustommerContact] DROP CONSTRAINT [' + @var27 + '];');
    ALTER TABLE [CustommerContact] ALTER COLUMN [CustomerID] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerGuarantorDetails_CustomerID] ON [CustommerGuarantorDetails] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerFinancialInfo_CustomerID] ON [CustommerFinancialInfo] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerEmployment_CustomerID] ON [CustommerEmployment] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_CustommerContact_CustomerID] ON [CustommerContact] ([CustomerID]) WHERE [CustomerID] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerContact] ADD CONSTRAINT [FK_CustommerContact_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerEmployment] ADD CONSTRAINT [FK_CustommerEmployment_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerFinancialInfo] ADD CONSTRAINT [FK_CustommerFinancialInfo_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    ALTER TABLE [CustommerGuarantorDetails] ADD CONSTRAINT [FK_CustommerGuarantorDetails_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250322130731_CusommerId_Nullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250322130731_CusommerId_Nullable', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250323185946_users_and_userRole_table_add'
)
BEGIN
    CREATE TABLE [User] (
        [UserId] int NOT NULL IDENTITY,
        [CompanyId] nvarchar(max) NULL,
        [FirstName] nvarchar(100) NULL,
        [LastName] nvarchar(100) NULL,
        [UserName] nvarchar(100) NOT NULL,
        [UserImage] nvarchar(max) NULL,
        [UserPassword] nvarchar(100) NOT NULL,
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
        CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250323185946_users_and_userRole_table_add'
)
BEGIN
    CREATE TABLE [UserRole] (
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
    WHERE [MigrationId] = N'20250323185946_users_and_userRole_table_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250323185946_users_and_userRole_table_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250324033129_Hrd_company_info_table_added'
)
BEGIN
    CREATE TABLE [HrdCompanyInfo] (
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
    WHERE [MigrationId] = N'20250324033129_Hrd_company_info_table_added'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250324033129_Hrd_company_info_table_added', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250324180930_user_table_name_change_to_users'
)
BEGIN
    ALTER TABLE [User] DROP CONSTRAINT [PK_User];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250324180930_user_table_name_change_to_users'
)
BEGIN
    EXEC sp_rename N'[User]', N'Users', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250324180930_user_table_name_change_to_users'
)
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250324180930_user_table_name_change_to_users'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250324180930_user_table_name_change_to_users', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250329164810_LoanType_Table_add'
)
BEGIN
    CREATE TABLE [LoanType] (
        [LoanTypeID] int NOT NULL IDENTITY,
        [LoanTypeName] nvarchar(100) NOT NULL,
        [Description] nvarchar(255) NULL,
        CONSTRAINT [PK_LoanType] PRIMARY KEY ([LoanTypeID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250329164810_LoanType_Table_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250329164810_LoanType_Table_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    DROP TABLE [LoanType];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [AccountBalance] (
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
        CONSTRAINT [FK_AccountBalance_CustommerPersonnelInfo_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [LoanPlan] (
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
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [PaymentMethod] (
        [PaytMethodID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_PaymentMethod] PRIMARY KEY ([PaytMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [Loan] (
        [LoanID] int NOT NULL IDENTITY,
        [LoanNumber] nvarchar(max) NOT NULL,
        [LoanAmount] decimal(18,2) NOT NULL,
        [PainAmount] decimal(18,2) NOT NULL,
        [DueAmount] decimal(18,2) NOT NULL,
        [TenureMonths] int NOT NULL,
        [LoanStartDate] datetime2 NOT NULL,
        [LoanEndDate] datetime2 NULL,
        [LoanStatus] tinyint NOT NULL,
        [Purpose] nvarchar(max) NULL,
        [DisbursementDate] datetime2 NULL,
        [CustomerID] int NOT NULL,
        [PayMethodId] int NULL,
        CONSTRAINT [PK_Loan] PRIMARY KEY ([LoanID]),
        CONSTRAINT [FK_Loan_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Loan_PaymentMethod_PayMethodId] FOREIGN KEY ([PayMethodId]) REFERENCES [PaymentMethod] ([PaytMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [LoanApplication] (
        [ApplicationID] int NOT NULL IDENTITY,
        [LoanAmount] decimal(18,2) NOT NULL,
        [RepaymentPeriod] int NOT NULL,
        [PurposeOfLoan] nvarchar(255) NOT NULL,
        [HasExistingLoans] bit NOT NULL,
        [ExistingLoanAmount] decimal(18,2) NULL,
        [LenderName] nvarchar(max) NULL,
        [MonthlyInstallments] decimal(18,2) NULL,
        [Status] bit NOT NULL,
        [ApplicationDate] datetime2 NOT NULL,
        [PlanID] int NOT NULL,
        [CustomerID] int NOT NULL,
        [PaytMethodID] int NULL,
        CONSTRAINT [PK_LoanApplication] PRIMARY KEY ([ApplicationID]),
        CONSTRAINT [FK_LoanApplication_CustommerPersonnelInfo_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanApplication_LoanPlan_PlanID] FOREIGN KEY ([PlanID]) REFERENCES [LoanPlan] ([PlanID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanApplication_PaymentMethod_PaytMethodID] FOREIGN KEY ([PaytMethodID]) REFERENCES [PaymentMethod] ([PaytMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [LoanInstalment] (
        [InstalmentID] int NOT NULL IDENTITY,
        [LoanID] int NOT NULL,
        [PaymentDate] date NOT NULL,
        [Status] tinyint NOT NULL,
        [PayMethodId] int NULL,
        [AccountId] int NULL,
        [AmountPaid] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_LoanInstalment] PRIMARY KEY ([InstalmentID]),
        CONSTRAINT [FK_LoanInstalment_AccountBalance_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [AccountBalance] ([Id]),
        CONSTRAINT [FK_LoanInstalment_Loan_LoanID] FOREIGN KEY ([LoanID]) REFERENCES [Loan] ([LoanID]) ON DELETE CASCADE,
        CONSTRAINT [FK_LoanInstalment_PaymentMethod_PayMethodId] FOREIGN KEY ([PayMethodId]) REFERENCES [PaymentMethod] ([PaytMethodID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE TABLE [Transaction] (
        [TransctionID] int NOT NULL IDENTITY,
        [TransactionType] nvarchar(max) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [TransactionDate] datetime2 NOT NULL,
        [CustomerId] int NOT NULL,
        [LoanID] int NULL,
        [PaytMethodID] int NOT NULL,
        [Remarks] nvarchar(max) NULL,
        CONSTRAINT [PK_Transaction] PRIMARY KEY ([TransctionID]),
        CONSTRAINT [FK_Transaction_CustommerPersonnelInfo_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Transaction_Loan_LoanID] FOREIGN KEY ([LoanID]) REFERENCES [Loan] ([LoanID]),
        CONSTRAINT [FK_Transaction_PaymentMethod_PaytMethodID] FOREIGN KEY ([PaytMethodID]) REFERENCES [PaymentMethod] ([PaytMethodID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE UNIQUE INDEX [IX_AccountBalance_AccountNo] ON [AccountBalance] ([AccountNo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_AccountBalance_CustomerId] ON [AccountBalance] ([CustomerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_Loan_CustomerID] ON [Loan] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_Loan_PayMethodId] ON [Loan] ([PayMethodId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_LoanApplication_CustomerID] ON [LoanApplication] ([CustomerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_LoanApplication_PaytMethodID] ON [LoanApplication] ([PaytMethodID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_LoanApplication_PlanID] ON [LoanApplication] ([PlanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_LoanInstalment_AccountId] ON [LoanInstalment] ([AccountId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_LoanInstalment_LoanID] ON [LoanInstalment] ([LoanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_LoanInstalment_PayMethodId] ON [LoanInstalment] ([PayMethodId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_Transaction_CustomerId] ON [Transaction] ([CustomerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_Transaction_LoanID] ON [Transaction] ([LoanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    CREATE INDEX [IX_Transaction_PaytMethodID] ON [Transaction] ([PaytMethodID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250331190001_loan_instalment_loan_plan_accountBanlance_PaymentMethod_Transction_model_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    ALTER TABLE [LoanApplication] DROP CONSTRAINT [FK_LoanApplication_PaymentMethod_PaytMethodID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    EXEC sp_rename N'[PaymentMethod].[PaytMethodID]', N'PayMethodID', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    EXEC sp_rename N'[LoanApplication].[PaytMethodID]', N'PayMethodID', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    EXEC sp_rename N'[LoanApplication].[IX_LoanApplication_PaytMethodID]', N'IX_LoanApplication_PayMethodID', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    DECLARE @var28 sysname;
    SELECT @var28 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LoanApplication]') AND [c].[name] = N'Status');
    IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [LoanApplication] DROP CONSTRAINT [' + @var28 + '];');
    ALTER TABLE [LoanApplication] ALTER COLUMN [Status] tinyint NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD CONSTRAINT [FK_LoanApplication_PaymentMethod_PayMethodID] FOREIGN KEY ([PayMethodID]) REFERENCES [PaymentMethod] ([PayMethodID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401041241_Loan_application_Status_type_as_byte'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250401041241_Loan_application_Status_type_as_byte', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [ApplyedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [ApplyedBy] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [ApprovedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [ApprovedBy] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [RejectAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [RejectedBy] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [UpdatedAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [UpdatedBy] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401060257_new_field_add_on_loan_application_approve_at'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250401060257_new_field_add_on_loan_application_approve_at', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401081024_loan_table_field_name_change_PaidAmount'
)
BEGIN
    EXEC sp_rename N'[Loan].[PainAmount]', N'PaidAmount', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250401081024_loan_table_field_name_change_PaidAmount'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250401081024_loan_table_field_name_change_PaidAmount', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406051510_PianId_include_in_Loan_table'
)
BEGIN
    ALTER TABLE [Loan] ADD [PlanID] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406051510_PianId_include_in_Loan_table'
)
BEGIN
    CREATE INDEX [IX_Loan_PlanID] ON [Loan] ([PlanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406051510_PianId_include_in_Loan_table'
)
BEGIN
    ALTER TABLE [Loan] ADD CONSTRAINT [FK_Loan_LoanPlan_PlanID] FOREIGN KEY ([PlanID]) REFERENCES [LoanPlan] ([PlanID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406051510_PianId_include_in_Loan_table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406051510_PianId_include_in_Loan_table', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406170543_loan_table_TotalPayableAmount_field_add'
)
BEGIN
    ALTER TABLE [Loan] ADD [TotalPayableAmount] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406170543_loan_table_TotalPayableAmount_field_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406170543_loan_table_TotalPayableAmount_field_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406171520_loan_table_total_intarest_monthlyInstalment_add'
)
BEGIN
    ALTER TABLE [Loan] ADD [MonthlyInstallment] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406171520_loan_table_total_intarest_monthlyInstalment_add'
)
BEGIN
    ALTER TABLE [Loan] ADD [TotalInterest] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250406171520_loan_table_total_intarest_monthlyInstalment_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250406171520_loan_table_total_intarest_monthlyInstalment_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250407195002_Depositamount_and_late_charge_add_on_loan_application'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [DepositAmount] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250407195002_Depositamount_and_late_charge_add_on_loan_application'
)
BEGIN
    ALTER TABLE [LoanApplication] ADD [LateCharge] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250407195002_Depositamount_and_late_charge_add_on_loan_application'
)
BEGIN
    ALTER TABLE [Loan] ADD [DepositAmount] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250407195002_Depositamount_and_late_charge_add_on_loan_application'
)
BEGIN
    ALTER TABLE [Loan] ADD [LateCharge] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250407195002_Depositamount_and_late_charge_add_on_loan_application'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250407195002_Depositamount_and_late_charge_add_on_loan_application', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250408191451_TransctionType_table_add'
)
BEGIN
    CREATE TABLE [TransactionType] (
        [TranTypeID] int NOT NULL IDENTITY,
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
        CONSTRAINT [PK_TransactionType] PRIMARY KEY ([TranTypeID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250408191451_TransctionType_table_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250408191451_TransctionType_table_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250408192049_TrancstionTypeId_field_name_update'
)
BEGIN
    EXEC sp_rename N'[TransactionType].[TranTypeID]', N'TransactionTypeID', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250408192049_TrancstionTypeId_field_name_update'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250408192049_TrancstionTypeId_field_name_update', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409165927_Deposit_table_add'
)
BEGIN
    CREATE TABLE [deposits] (
        [DepositID] int NOT NULL IDENTITY,
        [BankAccountNumber] nvarchar(20) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [RequestedDate] datetime2 NOT NULL,
        [IsApproved] bit NOT NULL,
        [BankTransactCode] nvarchar(100) NULL,
        [AdminRemarks] nvarchar(500) NULL,
        [ProcessedDate] datetime2 NULL,
        [ProcessedBy] nvarchar(50) NULL,
        [PaymentMethodID] int NOT NULL,
        [CustommerID] int NOT NULL,
        CONSTRAINT [PK_deposits] PRIMARY KEY ([DepositID]),
        CONSTRAINT [FK_deposits_CustommerPersonnelInfo_CustommerID] FOREIGN KEY ([CustommerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409165927_Deposit_table_add'
)
BEGIN
    CREATE INDEX [IX_deposits_CustommerID] ON [deposits] ([CustommerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409165927_Deposit_table_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250409165927_Deposit_table_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    ALTER TABLE [deposits] DROP CONSTRAINT [FK_deposits_CustommerPersonnelInfo_CustommerID];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    ALTER TABLE [deposits] DROP CONSTRAINT [PK_deposits];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    EXEC sp_rename N'[deposits]', N'Deposits', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    EXEC sp_rename N'[Deposits].[IX_deposits_CustommerID]', N'IX_Deposits_CustommerID', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    ALTER TABLE [Deposits] ADD CONSTRAINT [PK_Deposits] PRIMARY KEY ([DepositID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    CREATE TABLE [RechargePaymentMethod] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_RechargePaymentMethod] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    CREATE TABLE [RechargeAccount] (
        [Id] int NOT NULL IDENTITY,
        [RecPaymentMethodId] int NOT NULL,
        [BankOrWalletName] nvarchar(max) NOT NULL,
        [AccountName] nvarchar(max) NOT NULL,
        [AccountNumber] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_RechargeAccount] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_RechargeAccount_RechargePaymentMethod_RecPaymentMethodId] FOREIGN KEY ([RecPaymentMethodId]) REFERENCES [RechargePaymentMethod] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    CREATE INDEX [IX_RechargeAccount_RecPaymentMethodId] ON [RechargeAccount] ([RecPaymentMethodId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    ALTER TABLE [Deposits] ADD CONSTRAINT [FK_Deposits_CustommerPersonnelInfo_CustommerID] FOREIGN KEY ([CustommerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250409194425_recharge_payment_method_reacharge_account_table_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250409194425_recharge_payment_method_reacharge_account_table_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250412190937_rechargeResuest_table_create'
)
BEGIN
    DROP TABLE [Deposits];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250412190937_rechargeResuest_table_create'
)
BEGIN
    CREATE TABLE [Recharge] (
        [RechargeID] int NOT NULL IDENTITY,
        [BankAccountNumber] nvarchar(20) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [RequestedDate] datetime2 NOT NULL,
        [IsApproved] bit NOT NULL,
        [BankTransactCode] nvarchar(100) NULL,
        [AdminRemarks] nvarchar(500) NULL,
        [Statement] nvarchar(max) NULL,
        [ApproveAt] datetime2 NULL,
        [ApproveBy] int NULL,
        [PaymentMethodID] int NOT NULL,
        [BankId] int NOT NULL,
        [CustommerID] int NOT NULL,
        CONSTRAINT [PK_Recharge] PRIMARY KEY ([RechargeID]),
        CONSTRAINT [FK_Recharge_CustommerPersonnelInfo_CustommerID] FOREIGN KEY ([CustommerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250412190937_rechargeResuest_table_create'
)
BEGIN
    CREATE INDEX [IX_Recharge_CustommerID] ON [Recharge] ([CustommerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250412190937_rechargeResuest_table_create'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250412190937_rechargeResuest_table_create', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415045441_transction_data_type_change'
)
BEGIN
    DECLARE @var29 sysname;
    SELECT @var29 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transaction]') AND [c].[name] = N'TransactionType');
    IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Transaction] DROP CONSTRAINT [' + @var29 + '];');
    ALTER TABLE [Transaction] DROP COLUMN [TransactionType];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415045441_transction_data_type_change'
)
BEGIN
    ALTER TABLE [Transaction] ADD [TransactionType] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415045441_transction_data_type_change'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250415045441_transction_data_type_change', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250424212427_LateCharge_add_on_LianInstalment_table'
)
BEGIN
    DECLARE @var30 sysname;
    SELECT @var30 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Loan]') AND [c].[name] = N'LateCharge');
    IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Loan] DROP CONSTRAINT [' + @var30 + '];');
    ALTER TABLE [Loan] DROP COLUMN [LateCharge];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250424212427_LateCharge_add_on_LianInstalment_table'
)
BEGIN
    ALTER TABLE [LoanInstalment] ADD [LateCharge] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250424212427_LateCharge_add_on_LianInstalment_table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250424212427_LateCharge_add_on_LianInstalment_table', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426072946_withdraw_table_add'
)
BEGIN
    CREATE TABLE [Withdraw] (
        [WithdrawaID] int NOT NULL IDENTITY,
        [AccountNumber] nvarchar(20) NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [PaymentMethodID] int NOT NULL,
        [RequestedDate] datetime2 NOT NULL,
        [IsApproved] bit NOT NULL,
        [TransactionCode] nvarchar(100) NULL,
        [AdminRemarks] nvarchar(500) NULL,
        [ProcessedDate] datetime2 NULL,
        [ProcessedBy] nvarchar(50) NULL,
        [CustommerID] int NOT NULL,
        CONSTRAINT [PK_Withdraw] PRIMARY KEY ([WithdrawaID]),
        CONSTRAINT [FK_Withdraw_CustommerPersonnelInfo_CustommerID] FOREIGN KEY ([CustommerID]) REFERENCES [CustommerPersonnelInfo] ([CustomerID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426072946_withdraw_table_add'
)
BEGIN
    CREATE INDEX [IX_Withdraw_CustommerID] ON [Withdraw] ([CustommerID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426072946_withdraw_table_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250426072946_withdraw_table_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426073632_BankName_add_on_withdraw_table'
)
BEGIN
    ALTER TABLE [Withdraw] ADD [BankName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426073632_BankName_add_on_withdraw_table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250426073632_BankName_add_on_withdraw_table', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426075239_rejectAt_rejectBy_field_add'
)
BEGIN
    DECLARE @var31 sysname;
    SELECT @var31 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Withdraw]') AND [c].[name] = N'ProcessedBy');
    IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Withdraw] DROP CONSTRAINT [' + @var31 + '];');
    ALTER TABLE [Withdraw] DROP COLUMN [ProcessedBy];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426075239_rejectAt_rejectBy_field_add'
)
BEGIN
    EXEC sp_rename N'[Withdraw].[ProcessedDate]', N'RejectAt', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426075239_rejectAt_rejectBy_field_add'
)
BEGIN
    ALTER TABLE [Withdraw] ADD [ApproveAt] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426075239_rejectAt_rejectBy_field_add'
)
BEGIN
    ALTER TABLE [Withdraw] ADD [ApproveBy] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426075239_rejectAt_rejectBy_field_add'
)
BEGIN
    ALTER TABLE [Withdraw] ADD [RejectBy] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426075239_rejectAt_rejectBy_field_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250426075239_rejectAt_rejectBy_field_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426192922_IsApprove_null'
)
BEGIN
    DECLARE @var32 sysname;
    SELECT @var32 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Withdraw]') AND [c].[name] = N'IsApproved');
    IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Withdraw] DROP CONSTRAINT [' + @var32 + '];');
    ALTER TABLE [Withdraw] ALTER COLUMN [IsApproved] bit NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250426192922_IsApprove_null'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250426192922_IsApprove_null', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250429165328_Late_chargePaid_field_Add_on_instalment_table'
)
BEGIN
    ALTER TABLE [LoanInstalment] ADD [LateChargePaid] decimal(18,2) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250429165328_Late_chargePaid_field_Add_on_instalment_table'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250429165328_Late_chargePaid_field_Add_on_instalment_table', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503073845_EducationLavel_data_type_change'
)
BEGIN
    DECLARE @var33 sysname;
    SELECT @var33 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustommerPersonnelInfo]') AND [c].[name] = N'EducationLevel');
    IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [CustommerPersonnelInfo] DROP CONSTRAINT [' + @var33 + '];');
    ALTER TABLE [CustommerPersonnelInfo] ALTER COLUMN [EducationLevel] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503073845_EducationLavel_data_type_change'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250503073845_EducationLavel_data_type_change', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503075555_tbl_country_add'
)
BEGIN
    CREATE TABLE [TblCountry] (
        [CountryID] int NOT NULL IDENTITY,
        [CountryName] nvarchar(max) NULL,
        [TwoCharCountryCode] nvarchar(max) NULL,
        [ThreeCharCountryCode] nvarchar(max) NULL,
        CONSTRAINT [PK_TblCountry] PRIMARY KEY ([CountryID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250503075555_tbl_country_add'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250503075555_tbl_country_add', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506122508_dbUpload_onOnlineServer'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250506122508_dbUpload_onOnlineServer', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [Withdraw];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [Users];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [UserRole];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [TransactionType];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [Transaction];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [TblCountry];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [RechargePaymentMethod];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [RechargeAccount];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [Recharge];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [PaymentMethod];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [LoanPlan];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [LoanInstalment];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [LoanApplication];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [Loan];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [HrdCompanyInfo];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [CustommerPersonnelInfo];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [CustommerGuarantorDetails];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [CustommerFinancialInfo];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [CustommerEmployment];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [CustommerContact];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    ALTER SCHEMA [dbo] TRANSFER [AccountBalance];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250506152313_table_go_on_dbo'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250506152313_table_go_on_dbo', N'9.0.3');
END;

COMMIT;
GO

