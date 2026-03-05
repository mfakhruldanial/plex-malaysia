CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdRol] int,
  [FirstName] varchar(25),
  [LastName] varchar(25),
  [Email] varchar(50),
  [Password] varchar(255),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [Rol] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(25),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [Movie] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [PrimaryImage] varbinary(MAX),
  [Name] varchar(100),
  [Description] varchar(500),
  [Trailer] varchar(255),
  [Director] varchar(50),
  [Rating] int,
  [Premiere] datetime,
  [Duration] varchar(10),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [MovieCategory] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdMovie] int,
  [IdCategory] int,
  [Status] int
)
GO

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(50),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [MovieCast] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdMovie] int,
  [IdCast] int,
  [Role] varchar(50),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [Cast] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(50),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [Review] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdMovie] int,
  [IdUser] int,
  [Rating] int,
  [Comment] varchar(500),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [Watchlist] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdUser] int,
  [IdMovie] int,
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [Genre] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(50),
  [CreatedAt] datetime,
  [Status] int
)
GO

CREATE TABLE [MovieGenre] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdMovie] int,
  [IdGenre] int,
  [Status] int
)
GO

CREATE TABLE [MovieLike] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [IdUser] int,
  [IdMovie] int,
  [CreatedAt] datetime,
  [Status] int
)
GO

ALTER TABLE [User] ADD FOREIGN KEY ([IdRol]) REFERENCES [Rol] ([Id])
GO

ALTER TABLE [MovieCategory] ADD FOREIGN KEY ([IdMovie]) REFERENCES [Movie] ([Id])
GO

ALTER TABLE [MovieCategory] ADD FOREIGN KEY ([IdCategory]) REFERENCES [Category] ([Id])
GO

ALTER TABLE [MovieCast] ADD FOREIGN KEY ([IdMovie]) REFERENCES [Movie] ([Id])
GO

ALTER TABLE [MovieCast] ADD FOREIGN KEY ([IdCast]) REFERENCES [Cast] ([Id])
GO

ALTER TABLE [Review] ADD FOREIGN KEY ([IdMovie]) REFERENCES [Movie] ([Id])
GO

ALTER TABLE [Review] ADD FOREIGN KEY ([IdUser]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Watchlist] ADD FOREIGN KEY ([IdUser]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Watchlist] ADD FOREIGN KEY ([IdMovie]) REFERENCES [Movie] ([Id])
GO

ALTER TABLE [MovieGenre] ADD FOREIGN KEY ([IdMovie]) REFERENCES [Movie] ([Id])
GO

ALTER TABLE [MovieGenre] ADD FOREIGN KEY ([IdGenre]) REFERENCES [Genre] ([Id])
GO

ALTER TABLE [MovieLike] ADD FOREIGN KEY ([IdUser]) REFERENCES [User] ([Id])
GO

ALTER TABLE [MovieLike] ADD FOREIGN KEY ([IdMovie]) REFERENCES [Movie] ([Id])
GO
