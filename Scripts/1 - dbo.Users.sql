USE [FingerPrintApp]
GO

/****** Object: Table [dbo].[Users] Script Date: 29/10/2019 23:57:01 ******/

CREATE TABLE [dbo].[Users] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (80) NULL,
    [LastName]  VARCHAR (80) NULL,
    [BirthDate] DATE         NULL,
    [JobName]   VARCHAR (80) NULL,
    [Password]  VARCHAR (80) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);