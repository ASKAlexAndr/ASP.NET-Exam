﻿MERGE INTO [roles] AS Target
USING (VALUES 
('anonim'),
('admin'),
('user')
)
AS Source([Name])
ON Target.[Name] = Source.[Name]
WHEN MATCHED THEN
UPDATE SET
[Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name])
VALUES ([Name])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;