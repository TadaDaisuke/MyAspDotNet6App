USE [MyDatabase]
GO

DROP TABLE [dbo].[user]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user] (
    [user_id] INT NOT NULL
    ,[user_name] NVARCHAR(255) NOT NULL
    ,[mail_address] NVARCHAR(255) NULL
    ,[joined_date] DATE NULL
    );

INSERT INTO [dbo].[user]
VALUES (1, N'Suzuki Ichiro', N'Ichiro.Suzuki@example.com', '2010-01-01')
    ,(2, N'Sato Jiro', N'Jiro.Sato@example.com', '2020-07-01')
    ,(3, N'Tanaka Saburo', N'Tanaka3@example.com', '2022-05-01')
GO
