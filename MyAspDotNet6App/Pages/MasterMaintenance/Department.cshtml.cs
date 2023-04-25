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
        if (detailKey == null)
        {
            throw new ArgumentNullException(nameof(detailKey));
        }
        return Partial("DepartmentDetail", _departmentService.GetDepartment(detailKey));
    }

    public ContentResult OnPostSaveDetail([FromForm] Department? department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(department));
        }
        if (!ModelState.IsValid)
        {
            throw new InvalidDataException(nameof(department));
        }
        _departmentService.SaveDepartment(department);
        return Content("çXêVÇµÇ‹ÇµÇΩ");
    }
}
