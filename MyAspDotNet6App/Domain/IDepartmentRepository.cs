namespace MyAspDotNet6App.Domain;

public interface IDepartmentRepository
{
    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition searchCondition);

    public IEnumerable<Department> GetAllDepartments();

    public Department? GetDepartment(string departmentCode);

    public void SaveDepartment(Department department);

    public byte[] CreateExcelBytes(DepartmentSearchCondition searchCondition, string sheetName);
}
