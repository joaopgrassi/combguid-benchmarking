IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [TableWithExtendedUuidCreateSequential] (
    [Id] uniqueidentifier NOT NULL,
    [AnotherId] uniqueidentifier NOT NULL,
    [Value] nvarchar(400) NOT NULL,
    CONSTRAINT [PK_TableWithExtendedUuidCreateSequential] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TableWithNewSequentialIdAsDefault] (
    [Id] uniqueidentifier NOT NULL,
    [AnotherId] uniqueidentifier NOT NULL,
    [Value] nvarchar(400) NOT NULL,
    CONSTRAINT [PK_TableWithNewSequentialIdAsDefault] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TableWithRegularGuid] (
    [Id] uniqueidentifier NOT NULL,
    [AnotherId] uniqueidentifier NOT NULL,
    [Value] nvarchar(400) NOT NULL,
    CONSTRAINT [PK_TableWithRegularGuid] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TableWithSpanCustomGuidComb] (
    [Id] uniqueidentifier NOT NULL,
    [AnotherId] uniqueidentifier NOT NULL,
    [Value] nvarchar(400) NOT NULL,
    CONSTRAINT [PK_TableWithSpanCustomGuidComb] PRIMARY KEY ([Id])
);

GO

CREATE INDEX [IX_TableWithExtendedUuidCreateSequential_AnotherId_Value] ON [TableWithExtendedUuidCreateSequential] ([AnotherId], [Value]);

GO

CREATE INDEX [IX_TableWithNewSequentialIdAsDefault_AnotherId_Value] ON [TableWithNewSequentialIdAsDefault] ([AnotherId], [Value]);

GO

CREATE INDEX [IX_TableWithRegularGuid_AnotherId_Value] ON [TableWithRegularGuid] ([AnotherId], [Value]);

GO

CREATE INDEX [IX_TableWithSpanCustomGuidComb_AnotherId_Value] ON [TableWithSpanCustomGuidComb] ([AnotherId], [Value]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191224165413_Init', N'3.1.0');

GO

