
CREATE TABLE [dbo].[Contact] (
  [Id] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserId] int NOT NULL,
  [Name] varchar(128) NOT NULL,
  [Birthdate] date DEFAULT NULL,
  [Description] varchar(256) DEFAULT NULL,
  [Favorite] tinyint DEFAULT NULL,
  [GroupId] int NOT NULL DEFAULT 0,
  [CreatedAt] datetime NOT NULL,
  [UpdatedAt] datetime NOT NULL
)

CREATE NONCLUSTERED INDEX [UserId_Favorite_Name] ON [dbo].[Contact]
(
	[UserId] ASC,
	[Favorite] DESC,
	[Name] ASC
)

GO

CREATE TABLE [dbo].[Login] (
  [Id] binary(16) NOT NULL PRIMARY KEY,
  [UserId] int NOT NULL,
  [CreatedAt] datetime NOT NULL
)

CREATE TABLE [dbo].[User] (
  [Id] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  [UserName] varchar(128) NOT NULL,
  [Password] varbinary(256) NOT NULL,
  [Nonce] bigint NOT NULL,
  [CreatedAt] datetime NOT NULL,
  CONSTRAINT "UserName_UNIQUE" UNIQUE ([UserName])
)

GO
