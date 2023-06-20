namespace MyAspDotNet6App.Domain;

/// <summary>
/// 部署サービス実装クラス
/// </summary>
public class DepartmentService : IDepartmentService
{
    /// <summary>
    /// 部署リポジトリー
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="departmentRepository">部署リポジトリー</param>
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    /// <inheritdoc/>
    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition searchCondition)
        => _departmentRepository.SearchDepartments(searchCondition);

    /// <inheritdoc/>
    public IEnumerable<Department> GetAllDepartments()
        => _departmentRepository.GetAllDepartments();

    /// <inheritdoc/>
    public Department? GetDepartment(string departmentCode)
        => _departmentRepository.GetDepartment(departmentCode);

    /// <inheritdoc/>
    public void SaveDepartment(Department department)
        => _departmentRepository.SaveDepartment(department);

    /// <inheritdoc/>
    public byte[] DownloadDepartments(DepartmentSearchCondition searchCondition)
        => _departmentRepository.CreateExcelBytes(searchCondition, "Departments");
}
