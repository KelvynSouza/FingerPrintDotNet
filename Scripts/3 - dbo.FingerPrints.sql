USE [FingerPrintApp]
GO

/****** Object: Table [dbo].[FingerPrints] Script Date: 29/10/2019 23:56:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FingerPrints] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [FingerPrintImage] VARBINARY (MAX) NULL,
    [UserId]           INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


