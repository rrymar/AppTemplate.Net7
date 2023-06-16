IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
	CREATE TABLE [__EFMigrationsHistory] (
		[MigrationId] nvarchar(150) NOT NULL,
		[ProductVersion] nvarchar(32) NOT NULL,
		CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
	);
END;

SET ANSI_PADDING ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(30) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [IsActive] bit NOT NULL,
        [CreatedOn] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedOn] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [Username] nvarchar(32) NULL,
        [FirstName] nvarchar(50) NULL,
        [LastName] nvarchar(50) NULL,
        [FullName] AS CONCAT(FirstName,' ', LastName),
        [Email] nvarchar(200) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE TABLE [UserRoles] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE INDEX [IX_Users_IsActive] ON [Users] ([IsActive]);
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    CREATE INDEX [IX_Users_Username] ON [Users] ([Username]);
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109083415_AddUsers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200109083415_AddUsers', N'3.1.0');
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109090709_AddSystemUser')
BEGIN
    INSERT INTO [dbo].[Users] 
           ([Username]
           ,[FirstName]
           ,[LastName]
           ,[IsActive])
    VALUES ('system'
           ,'System'
           ,'Auto'
           ,1);
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109090709_AddSystemUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200109090709_AddSystemUser', N'3.1.0');
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200727112503_AddUsersPreferences')
BEGIN
    CREATE TABLE [UserPreferenses] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_UserPreferenses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserPreferenses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200727112503_AddUsersPreferences')
BEGIN
    CREATE INDEX [IX_UserPreferenses_UserId] ON [UserPreferenses] ([UserId]);
END;

GO

IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200727112503_AddUsersPreferences')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200727112503_AddUsersPreferences', N'3.1.6');
END;

GO

IF @@TRANCOUNT > 0
	COMMIT TRANSACTION
ELSE
	PRINT 'Transaction should be rolled back by XACT_ABORT property'
GO

