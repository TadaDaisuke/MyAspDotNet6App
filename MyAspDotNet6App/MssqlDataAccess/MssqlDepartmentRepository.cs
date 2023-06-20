using Microsoft.Data.SqlClient;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.Utilities;
using System.Data;

namespace MyAspDotNet6App.MssqlDataAccess;

/// <summary>
/// 部署リポジトリー実装クラス
/// </summary>
public class MssqlDepartmentRepository : IDepartmentRepository
{
    /// <summary>
    /// MyDatabaseアクセス関連のコンテキスト
    /// </summary>
    private readonly MyDatabaseContext _context;

    /// <summary>
    /// Excel生成ユーティリティー
    /// </summary>
    private readonly IExcelCreator _excelCreator;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="context">MyDatabaseアクセス関連のコンテキスト</param>
    /// <param name="excelCreator">Excel生成ユーティリティー</param>
    public MssqlDepartmentRepository(MyDatabaseContext context, IExcelCreator excelCreator)
    {
        _context = context;
        _excelCreator = excelCreator;
    }

    /// <inheritdoc/>
    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition searchCondition)
    {
        var cmd = new SqlCommand("sp_search_departments") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_name_part", SqlDbType.NVarChar, searchCondition.DepartmentNamePart)
            .AddParameter("@sort_item", SqlDbType.NVarChar, searchCondition.SortItem)
            .AddParameter("@sort_type", SqlDbType.NVarChar, searchCondition.SortType)
            .AddParameter("@offset_rows", SqlDbType.Int, searchCondition.OffsetRows)
            .AddParameter("@fetch_rows", SqlDbType.Int, _context.FETCH_ROW_SIZE);
        return _context.GetRowList(cmd)
            .Select(row =>
                new DepartmentListRow(
                    department: new Department { DepartmentCode = row["department_code"], DepartmentName = row["department_name"] },
                    seq: row["seq"].ToInt(),
                    totalRecordsCount: row["total_records_count"].ToInt()))
            .ToList();
    }

    /// <inheritdoc/>
    public IEnumerable<Department> GetAllDepartments()
    {
        var cmd = new SqlCommand("sp_search_departments") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_name_part", SqlDbType.NVarChar, null)
            .AddParameter("@sort_item", SqlDbType.NVarChar, null)
            .AddParameter("@sort_type", SqlDbType.NVarChar, null)
            .AddParameter("@offset_rows", SqlDbType.Int, 0)
            .AddParameter("@fetch_rows", SqlDbType.Int, int.MaxValue);
        return _context.GetRowList(cmd)
            .Select(row => new Department { DepartmentCode = row["department_code"], DepartmentName = row["department_name"] })
            .ToList();
    }

    /// <inheritdoc/>
    public Department? GetDepartment(string departmentCode)
    {
        var cmd = new SqlCommand("sp_get_department") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_code", SqlDbType.NVarChar, departmentCode);
        return _context.GetRowList(cmd)
            .Select(row => new Department { DepartmentCode = row["department_code"], DepartmentName = row["department_name"] })
            .FirstOrDefault();
    }

    /// <inheritdoc/>
    public void SaveDepartment(Department department)
    {
        var cmd = new SqlCommand("sp_save_department") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_code", SqlDbType.NVarChar, department.DepartmentCode)
            .AddParameter("@department_name", SqlDbType.NVarChar, department.DepartmentName)
            .AddOutputParameter("@error_message", SqlDbType.NVarChar, 4000);
        var errorMessage = _context.ExecuteSql(cmd).Parameters["@error_message"].Value.ToString();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new Exception(errorMessage);
        }
    }

    /// <inheritdoc/>
    public byte[] CreateExcelBytes(DepartmentSearchCondition searchCondition, string sheetName)
    {
        var cmd = new SqlCommand("sp_download_departments") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_name_part", SqlDbType.NVarChar, searchCondition.DepartmentNamePart)
            .AddParameter("@sort_item", SqlDbType.NVarChar, searchCondition.SortItem)
            .AddParameter("@sort_type", SqlDbType.NVarChar, searchCondition.SortType);
        return _excelCreator.CreateFileBytes(cmd, sheetName);
    }
}
