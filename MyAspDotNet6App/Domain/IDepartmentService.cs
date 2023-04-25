namespace MyAspDotNet6App.Domain;

public interface IDepartmentService
{
    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition? condition);

    public Department? GetDepartment(string departmentCode);

    public void SaveDepartment(Department department);
}
