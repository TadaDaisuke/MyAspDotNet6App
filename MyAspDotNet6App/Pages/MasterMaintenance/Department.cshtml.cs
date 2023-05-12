using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAspDotNet6App.Domain;

namespace MyAspDotNet6App.Pages.MasterMaintenance;

public class DepartmentModel : PageModel
{
    private readonly IDepartmentService _departmentService;

    public DepartmentModel(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [BindProperty]
    public DepartmentSearchCondition SearchCondition { get; set; } = new DepartmentSearchCondition();

    public void OnGet()
    {
    }

    public PartialViewResult OnPostSearch()
    {
        var departments = _departmentService.SearchDepartments(SearchCondition);
        Response.Headers.Add("X-total-records-count", (departments.FirstOrDefault()?.TotalRecordsCount ?? 0).ToString());
        Response.Headers.Add("X-last-seq", departments.Max(x => x?.Seq)?.ToString());
        return Partial("DepartmentList", departments);
    }

    public PartialViewResult OnPostGetDetail([FromForm] string? detailKey)
    {
        ArgumentNullException.ThrowIfNull(detailKey);
        return Partial("DepartmentDetail", _departmentService.GetDepartment(detailKey));
    }

    public PartialViewResult OnPostGetBlankDetail()
    {
        return Partial("DepartmentDetail", new Department { DepartmentCode = "（新規）" });
    }

    public ContentResult OnPostSaveDetail([FromForm] Department? department)
    {
        ArgumentNullException.ThrowIfNull(department);
        if (!ModelState.IsValid)
        {
            throw new InvalidDataException(nameof(department));
        }
        _departmentService.SaveDepartment(department);
        return Content("更新しました");
    }

    public FileContentResult OnPostDownloadExcel()
    {
        var bytes = _departmentService.DownloadDepartments(SearchCondition);
        Response.Headers.Add("X-download-file-name", $"Departments_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        return File(bytes, CONTENT_TYPE_XLSX);
    }
}
