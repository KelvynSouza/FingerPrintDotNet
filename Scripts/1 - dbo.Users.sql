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


SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [FirstName], [LastName], [BirthDate], [JobName], [Password]) VALUES (1009, N'Admin', N'Principal', N'2019-10-29', N'Desenvolvedor', N'admin')
INSERT INTO [dbo].[Users] ([Id], [FirstName], [LastName], [BirthDate], [JobName], [Password]) VALUES (2015, N'asd', N'asd', N'1999-06-30', N'asd', N'123')
SET IDENTITY_INSERT [dbo].[Users] OFF
