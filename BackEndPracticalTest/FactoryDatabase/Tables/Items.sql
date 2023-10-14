CREATE TABLE [dbo].[Items] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)    NOT NULL,
    [Description] VARCHAR (100)   NOT NULL,
    [Price]       DECIMAL (18, 2) NOT NULL
);


