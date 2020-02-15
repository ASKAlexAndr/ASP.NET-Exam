CREATE TABLE [dbo].[roles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(40) NOT NULL, 
    CONSTRAINT [AK_roles_name] UNIQUE ([Name])
)

