using MyAspDotNet6App.Utilities;

namespace MyAspDotNet6App.Domain;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IExcelCreator _excelCreator;

    public DepartmentService(IDepartmentRepository departmentRepository, IExcelCreator excelCreator)
    {
        _departmentRepository = departmentRepository;
        _excelCreator = excelCreator;
    }

    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition? condition)
        => _departmentRepository.SearchDepartments(condition);

    public IEnumerable<Department> GetAllDepartments()
        => _departmentRepository.GetAllDepartments();

    public Department? GetDepartment(string departmentCode)
        => _departmentRepository.GetDepartment(departmentCode);

    public void SaveDepartment(Department department)
        => _departmentRepository.SaveDepartment(department);

    public byte[] DownloadDepartments(DepartmentSearchCondition? condition)
        => _excelCreator.CreateFileBytes(_departmentRepository.GetDownloadCommand(condition), "Departments");
}
