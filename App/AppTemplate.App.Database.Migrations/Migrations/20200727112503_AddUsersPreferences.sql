IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200727112503_AddUsersPreferences')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200727112503_AddUsersPreferences')
BEGIN
    CREATE INDEX [IX_UserPreferenses_UserId] ON [UserPreferenses] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200727112503_AddUsersPreferences')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200727112503_AddUsersPreferences', N'3.1.6');
END;

GO

