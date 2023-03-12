USE [MyDatabase]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user] (
    [user_id] INT NOT NULL
    ,[user_name] NVARCHAR(255) NOT NULL
    ,[mail_address] NVARCHAR(255) NULL
    );
