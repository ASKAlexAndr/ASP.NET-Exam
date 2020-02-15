CREATE TABLE [dbo].[roles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(40) NOT NULL, 
    CONSTRAINT [AK_roles_name] UNIQUE ([name])
)
