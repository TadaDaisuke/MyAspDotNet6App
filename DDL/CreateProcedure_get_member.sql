USE [MyDatabase]
GO

CREATE OR ALTER PROCEDURE dbo.sp_get_member (@member_code NVARCHAR(8))
AS
SELECT TOP 1 member_code
    ,given_name
    ,family_name
    ,given_name_kana
    ,family_name_kana
    ,given_name_kanji
    ,family_name_kanji
    ,mail_address
    ,joined_date
FROM member
WHERE member_code = @member_code
GO
