MERGE INTO [roles] AS Target
USING (VALUES 
('anonim'),
('admin'),
('user')
)
AS Source([name])
ON Target.[name] = Source.[name]
WHEN MATCHED THEN
UPDATE SET
[name] = Source.[name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([name])
VALUES ([name])
WHEN NOT MATCHED BY SOURCE THEN
DELETE;