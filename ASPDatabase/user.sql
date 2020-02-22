CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Login] NVARCHAR(40) NOT NULL, 
    [Name] NVARCHAR(40) NOT NULL, 
    [Surname] NVARCHAR(40) NOT NULL, 
    [roleId] INT NULL, 
    CONSTRAINT [AK_user_login] UNIQUE ([Login]), 
    CONSTRAINT [FK_user_role] FOREIGN KEY ([roleId]) REFERENCES [Role]([Id])
)
