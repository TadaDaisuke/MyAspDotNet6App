USE [MyDatabase]
GO

CREATE OR ALTER FUNCTION dbo.tvf_search_members (
    @member_name_part NVARCHAR(128)
    ,@joined_date_from DATE
    ,@joined_date_to DATE
    )
RETURNS TABLE
AS
RETURN (
        SELECT *
        FROM member
        WHERE (
                @member_name_part IS NULL
                OR given_name LIKE N'%' + @member_name_part + N'%'
                OR family_name LIKE N'%' + @member_name_part + N'%'
                OR given_name_kana LIKE N'%' + @member_name_part + N'%'
                OR family_name_kana LIKE N'%' + @member_name_part + N'%'
                OR given_name_kanji LIKE N'%' + @member_name_part + N'%'
                OR family_name_kanji LIKE N'%' + @member_name_part + N'%'
                )
            AND (
                @joined_date_from IS NULL
                OR @joined_date_from <= joined_date
                )
            AND (
                @joined_date_to IS NULL
                OR joined_date <= @joined_date_to
                )
        )
GO

CREATE OR ALTER PROCEDURE dbo.sp_search_members (
    @member_name_part NVARCHAR(128)
    ,@joined_date_from DATE
    ,@joined_date_to DATE
    ,@sort_item NVARCHAR(128)
    ,@sort_type NVARCHAR(4)
    ,@offset_rows INT
    ,@fetch_rows INT
    )
AS
SELECT ROW_NUMBER() OVER (
        ORDER BY IIF(@sort_item = N'joined_date' AND @sort_type = N'asc', joined_date, NULL) ASC
            ,IIF(@sort_item = N'joined_date' AND @sort_type = N'desc', joined_date, NULL) DESC
            ,IIF(@sort_item = N'member_code' AND @sort_type = N'asc', member_code, NULL) ASC
            ,IIF(@sort_item = N'member_code' AND @sort_type = N'desc', member_code, NULL) DESC
        ) AS seq
    ,member_code
    ,given_name
    ,family_name
    ,given_name_kana
    ,family_name_kana
    ,given_name_kanji
    ,family_name_kanji
    ,mail_address
    ,joined_date
    ,(
        SELECT COUNT(*)
        FROM tvf_search_members(@member_name_part, @joined_date_from, @joined_date_to)
        ) AS total_records_count
FROM tvf_search_members(@member_name_part, @joined_date_from, @joined_date_to)
ORDER BY seq OFFSET @offset_rows ROWS
FETCH NEXT @fetch_rows ROWS ONLY
GO
