CREATE TABLE [dbo].[Items]
(
	[ID] INT IDENTITY PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NOT NULL, 
    [Price] FLOAT NOT NULL
)
