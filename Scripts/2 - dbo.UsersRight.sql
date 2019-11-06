USE [FingerPrintApp]
GO

/****** Object: Table [dbo].[UsersRight] Script Date: 29/10/2019 23:57:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsersRight] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Read]   BIT DEFAULT ((0)) NOT NULL,
    [Write]  BIT DEFAULT ((0)) NOT NULL,
    [Delete] BIT DEFAULT ((0)) NOT NULL,
    [UserID] INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([UserID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([Id])
);



SET IDENTITY_INSERT [dbo].[UsersRight] ON
INSERT INTO [dbo].[UsersRight] ([Id], [Read], [Write], [Delete], [UserID]) VALUES (1, 1, 1, 1, 1009)
INSERT INTO [dbo].[UsersRight] ([Id], [Read], [Write], [Delete], [UserID]) VALUES (2, 1, 1, 0, 2015)
SET IDENTITY_INSERT [dbo].[UsersRight] OFF

