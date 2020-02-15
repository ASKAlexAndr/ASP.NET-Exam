﻿CREATE TABLE [dbo].[users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [login] NVARCHAR(40) NOT NULL, 
    [name] NVARCHAR(40) NOT NULL, 
    [surname] NVARCHAR(40) NOT NULL, 
    [role_id] INT NOT NULL, 
    CONSTRAINT [AK_users_login] UNIQUE ([login]), 
    CONSTRAINT [FK_users_role] FOREIGN KEY ([role_id]) REFERENCES [roles]([id])
)