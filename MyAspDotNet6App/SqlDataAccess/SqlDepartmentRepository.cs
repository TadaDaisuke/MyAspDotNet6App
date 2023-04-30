using Microsoft.Data.SqlClient;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.SqlDataAccess.Common;
using System.Data;

namespace MyAspDotNet6App.SqlDataAccess;

public class SqlDepartmentRepository : IDepartmentRepository
{
    private readonly MyAppContext _context;

    public SqlDepartmentRepository(MyAppContext context)
    {
        _context = context;
    }

    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition? departmentSearchCondition)
    {
        var cmd = new SqlCommand("sp_search_departments") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_name_part", SqlDbType.NVarChar, departmentSearchCondition?.DepartmentNamePart)
            .AddParameter("@sort_item", SqlDbType.NVarChar, departmentSearchCondition?.SortItem)
            .AddParameter("@sort_type", SqlDbType.NVarChar, departmentSearchCondition?.SortType)
            .AddParameter("@offset_rows", SqlDbType.Int, departmentSearchCondition?.OffsetRows ?? 0)
            .AddParameter("@fetch_rows", SqlDbType.Int, _context.FETCH_ROW_SIZE);
        return _context.GetRowList(cmd)
            .Select(row =>
                new DepartmentListRow(
                    department: new Department { DepartmentCode = row["department_code"], DepartmentName = row["department_name"] },
                    seq: row["seq"].ToInt(),
                    totalRecordsCount: row["total_records_count"].ToInt()))
            .ToList();
    }

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

    public Department? GetDepartment(string departmentCode)
    {
        var cmd = new SqlCommand("sp_get_department") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@department_code", SqlDbType.NVarChar, departmentCode);
        return _context.GetRowList(cmd)
            .Select(row => new Department { DepartmentCode = row["department_code"], DepartmentName = row["department_name"] })
            .FirstOrDefault();
    }

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
}
