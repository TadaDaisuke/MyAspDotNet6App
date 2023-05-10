﻿namespace MyAspDotNet6App.Domain;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
        => _departmentRepository = departmentRepository;

    public IEnumerable<DepartmentListRow> SearchDepartments(DepartmentSearchCondition? condition)
        => _departmentRepository.SearchDepartments(condition);

    public IEnumerable<Department> GetAllDepartments()
        => _departmentRepository.GetAllDepartments();

    public Department? GetDepartment(string departmentCode)
        => _departmentRepository.GetDepartment(departmentCode);

    public void SaveDepartment(Department department)
        => _departmentRepository.SaveDepartment(department);

    public byte[] DownloadDepartments(DepartmentSearchCondition? condition)
        => _departmentRepository.DownloadDepartments(condition);
}
