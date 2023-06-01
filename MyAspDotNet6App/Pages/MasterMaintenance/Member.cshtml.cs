using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.PageParts;

namespace MyAspDotNet6App.Pages.MasterMaintenance;

public class MemberModel : PageModel
{
    private readonly IMemberService _memberService;

    public IEnumerable<SelectListItem> DepartmentListItems;

    public IEnumerable<RadioItem> EmailDomains;

    [BindProperty]
    public MemberSearchCondition SearchCondition { get; set; } = new MemberSearchCondition();

    public MemberModel(IMemberService memberService, IDepartmentService departmentService)
    {
        _memberService = memberService;
        DepartmentListItems = departmentService.GetAllDepartments()
            .Select(x => new SelectListItem { Value = x.DepartmentCode, Text = x.DepartmentName })
            .ToList();
        EmailDomains = new List<RadioItem>
        {
            new RadioItem("指定なし", "", true),
            new RadioItem(".com", ".com"),
            new RadioItem(".net", ".net"),
            new RadioItem(".org", ".org"),
            new RadioItem(".co.jp", ".co.jp"),
            new RadioItem(".ne.jp", ".ne.jp"),
        };
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
        ArgumentNullException.ThrowIfNull(detailKey);
        var member = _memberService.GetMember(detailKey) ?? throw new InvalidOperationException(detailKey.ToString());
        member.DepartmentListItems = DepartmentListItems.ToList();
        return Partial("MemberDetail", member);
    }

    public PartialViewResult OnPostGetBlankDetail()
    {
        var member = new Member
        {
            MemberCode = "（新規）",
            DepartmentListItems = DepartmentListItems.ToList()
        };
        return Partial("MemberDetail", member);
    }

    public ContentResult OnPostSaveDetail([FromForm] Member? member)
    {
        ArgumentNullException.ThrowIfNull(member);
        if (!ModelState.IsValid)
        {
            throw new InvalidDataException(nameof(member));
        }
        _memberService.SaveMember(member);
        return Content("更新しました");
    }

    public FileContentResult OnPostDownloadExcel()
    {
        var bytes = _memberService.DownloadMembers(SearchCondition);
        Response.Headers.Add("X-download-file-name", $"Members_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        return File(bytes, CONTENT_TYPE_XLSX);
    }

    public JsonResult OnPostSuggestMemberCode(string? memberCodePart)
    {
        ArgumentNullException.ThrowIfNull(memberCodePart);
        var list = _memberService.SuggestMemberCode(memberCodePart);
        return new JsonResult(list);
    }
}
