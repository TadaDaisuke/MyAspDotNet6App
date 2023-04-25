using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAspDotNet6App.Domain;

namespace MyAspDotNet6App.Pages.MasterMaintenance;

public class MemberModel : PageModel
{
    private readonly IMemberService _memberService;

    public MemberModel(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [BindProperty]
    public MemberSearchCondition SearchCondition { get; set; } = new MemberSearchCondition();

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
        return Partial("MemberDetail", _memberService.GetMember(detailKey));
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
