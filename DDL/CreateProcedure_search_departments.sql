USE [MyDatabase]
GO

CREATE OR ALTER FUNCTION dbo.tvf_search_departments (@department_name_part NVARCHAR(128))
RETURNS TABLE
AS
RETURN (
        SELECT *
        FROM department
        WHERE @department_name_part IS NULL
            OR department_name LIKE N'%' + @department_name_part + N'%'
        )
GO

CREATE OR ALTER PROCEDURE dbo.sp_search_departments (
    @department_name_part NVARCHAR(128)
    ,@sort_item NVARCHAR(128)
    ,@sort_type NVARCHAR(4)
    ,@offset_rows INT
    ,@fetch_rows INT
    )
AS
SELECT ROW_NUMBER() OVER (
        ORDER BY IIF(@sort_item = N'department_name' AND @sort_type = N'asc', department_name, NULL) ASC
            ,IIF(@sort_item = N'department_name' AND @sort_type = N'desc', department_name, NULL) DESC
            ,IIF(@sort_item = N'department_code' AND @sort_type = N'asc', department_code, NULL) ASC
            ,IIF(@sort_item = N'department_code' AND @sort_type = N'desc', department_code, NULL) DESC
        ) AS seq
    ,department_code
    ,department_name
    ,(
        SELECT COUNT(*)
        FROM tvf_search_departments(@department_name_part)
        ) AS total_records_count
FROM tvf_search_departments(@department_name_part)
ORDER BY seq
OFFSET @offset_rows ROWS
FETCH NEXT @fetch_rows ROWS ONLY
GO
