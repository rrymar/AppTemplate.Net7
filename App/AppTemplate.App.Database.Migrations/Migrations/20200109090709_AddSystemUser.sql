IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109090709_AddSystemUser')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200109090709_AddSystemUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200109090709_AddSystemUser', N'3.1.0');
END;

GO

