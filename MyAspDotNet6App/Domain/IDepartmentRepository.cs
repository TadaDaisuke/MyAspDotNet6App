namespace MyAspDotNet6App.Domain;

public interface IDepartmentRepository
{
    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition? condition);

    public IEnumerable<Department> GetAllDepartments();

    public Department? GetDepartment(string departmentCode);

    public void SaveDepartment(Department department);

    public byte[] DownloadDepartments(DepartmentSearchCondition? condition);
}
