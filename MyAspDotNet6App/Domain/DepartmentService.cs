namespace MyAspDotNet6App.Domain;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition searchCondition)
        => _departmentRepository.SearchDepartments(searchCondition);

    public IEnumerable<Department> GetAllDepartments()
        => _departmentRepository.GetAllDepartments();

    public Department? GetDepartment(string departmentCode)
        => _departmentRepository.GetDepartment(departmentCode);

    public void SaveDepartment(Department department)
        => _departmentRepository.SaveDepartment(department);

    public byte[] DownloadDepartments(DepartmentSearchCondition searchCondition)
        => _departmentRepository.CreateExcelBytes(searchCondition, "Departments");
}
