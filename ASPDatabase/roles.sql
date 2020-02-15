CREATE TABLE [dbo].[roles]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(40) NOT NULL, 
    CONSTRAINT [AK_roles_name] UNIQUE ([name])
)

