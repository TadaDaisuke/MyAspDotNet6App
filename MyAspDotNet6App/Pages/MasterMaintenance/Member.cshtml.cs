using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspDotNet6App.Domain;

namespace MyAspDotNet6App.Pages.MasterMaintenance;

public class MemberModel : PageModel
{
    private readonly IMemberService _memberService;

    public IEnumerable<SelectListItem> DepartmentListItems;

    [BindProperty]
    public MemberSearchCondition SearchCondition { get; set; } = new MemberSearchCondition();

    public MemberModel(IMemberService memberService, IDepartmentService departmentService)
    {
        _memberService = memberService;
        DepartmentListItems = departmentService.GetAllDepartments()
            .Select(x => new SelectListItem { Value = x.DepartmentCode, Text = x.DepartmentName })
            .ToList();
    }

    public void OnGet()
    {
    }

    public PartialViewResult OnPostSearch()
    {
        var members = _memberService.SearchMembers(SearchCondition);
        Response.Headers.Add("X-total-records-count", (members.FirstOrDefault()?.TotalRecordsCount ?? 0).ToString());
        Response.Headers.Add("X-last-seq", members.Max(x => x?.Seq)?.ToString());
        return Partial("MemberList", members);
    }

    public PartialViewResult OnPostGetDetail([FromForm] string? detailKey)
    {
        if (detailKey == null)
        {
            throw new ArgumentNullException(nameof(detailKey));
        }
        var member = _memberService.GetMember(detailKey) ?? throw new InvalidOperationException(detailKey.ToString());
        member.DepartmentListItems = DepartmentListItems.ToList();
        return Partial("MemberDetail", member);
    }

    public ContentResult OnPostSaveDetail([FromForm] Member? member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member));
        }
        if (!ModelState.IsValid)
        {
            throw new InvalidDataException(nameof(member));
        }
        _memberService.SaveMember(member);
        return Content("更新しました");
    }
}
