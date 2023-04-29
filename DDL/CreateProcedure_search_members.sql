USE [MyDatabase]
GO

CREATE OR ALTER FUNCTION dbo.tvf_search_members (
    @member_name_part NVARCHAR(128)
    ,@joined_date_from DATE
    ,@joined_date_to DATE
    ,@department_code NVARCHAR(6)
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
            AND (
                @department_code IS NULL
                OR department_code = @department_code
                )
        )
GO

CREATE OR ALTER PROCEDURE dbo.sp_search_members (
    @member_name_part NVARCHAR(128)
    ,@joined_date_from DATE
    ,@joined_date_to DATE
    ,@department_code NVARCHAR(6)
    ,@sort_item NVARCHAR(128)
    ,@sort_type NVARCHAR(4)
    ,@offset_rows INT
    ,@fetch_rows INT
    )
AS
SELECT ROW_NUMBER() OVER (
        ORDER BY IIF(@sort_item = N'joined_date' AND @sort_type = N'asc', mbr.joined_date, NULL) ASC
            ,IIF(@sort_item = N'joined_date' AND @sort_type = N'desc', mbr.joined_date, NULL) DESC
            ,IIF(@sort_item = N'member_code' AND @sort_type = N'asc', mbr.member_code, NULL) ASC
            ,IIF(@sort_item = N'member_code' AND @sort_type = N'desc', mbr.member_code, NULL) DESC
            ,IIF(@sort_item = N'name_kana' AND @sort_type = N'asc', mbr.family_name_kana + N' ' + mbr.given_name_kana, NULL) ASC
            ,IIF(@sort_item = N'name_kana' AND @sort_type = N'desc', mbr.family_name_kana + N' ' + mbr.given_name_kana, NULL) DESC
            ,IIF(@sort_item = N'name_english' AND @sort_type = N'asc', mbr.given_name + N' ' + mbr.family_name, NULL) ASC
            ,IIF(@sort_item = N'name_english' AND @sort_type = N'desc', mbr.given_name + N' ' + mbr.family_name, NULL) DESC
            ,IIF(@sort_item = N'department_code' AND @sort_type = N'asc', mbr.department_code, NULL) ASC
            ,IIF(@sort_item = N'department_code' AND @sort_type = N'desc', mbr.department_code, NULL) DESC
        ) AS seq
    ,mbr.member_code
    ,mbr.given_name
    ,mbr.family_name
    ,mbr.given_name_kana
    ,mbr.family_name_kana
    ,mbr.given_name_kanji
    ,mbr.family_name_kanji
    ,mbr.mail_address
    ,mbr.joined_date
    ,dpt.department_name
    ,(
        SELECT COUNT(*)
        FROM tvf_search_members(@member_name_part, @joined_date_from, @joined_date_to, @department_code)
        ) AS total_records_count
FROM tvf_search_members(@member_name_part, @joined_date_from, @joined_date_to, @department_code) AS mbr
INNER JOIN department AS dpt
    ON dpt.department_code = mbr.department_code
ORDER BY seq OFFSET @offset_rows ROWS
FETCH NEXT @fetch_rows ROWS ONLY
GO
