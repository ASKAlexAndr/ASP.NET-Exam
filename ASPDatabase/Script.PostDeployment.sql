MERGE INTO [Role] AS Target
USING (VALUES 
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