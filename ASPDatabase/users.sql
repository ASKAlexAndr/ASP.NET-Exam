CREATE TABLE [dbo].[users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Login] NVARCHAR(40) NOT NULL, 
    [Name] NVARCHAR(40) NOT NULL, 
    [Surname] NVARCHAR(40) NOT NULL, 
    [roleId] INT NULL, 
    CONSTRAINT [AK_users_login] UNIQUE ([Login]), 
    CONSTRAINT [FK_users_role] FOREIGN KEY ([roleId]) REFERENCES [roles]([Id])
)
