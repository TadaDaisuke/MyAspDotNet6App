using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.PageParts;

namespace MyAspDotNet6App.Pages.MasterMaintenance;

/// <summary>
/// Memberページモデル
/// </summary>
public class MemberModel : PageModel
{
    /// <summary>
    /// メンバーサービス
    /// </summary>
    private readonly IMemberService _memberService;

    /// <summary>
    /// 部署ドロップダウンリストアイテムのコレクション
    /// </summary>
    public IEnumerable<SelectListItem> DepartmentListItems { get; private set; }

    /// <summary>
    /// 電子メールドメインラジオボタンアイテムのコレクション
    /// </summary>
    public IEnumerable<RadioItem> EmailDomains { get; private set; }

    /// <summary>
    /// メンバー検索条件
    /// </summary>
    [BindProperty]
    public MemberSearchCondition SearchCondition { get; set; } = new MemberSearchCondition();

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="memberService">メンバーサービス</param>
    /// <param name="departmentService">部署サービス</param>
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

    /// <summary>
    /// GETリクエストハンドラー
    /// </summary>
    public void OnGet()
    {
    }

    /// <summary>
    /// Search（POST）リクエストハンドラー
    /// </summary>
    public PartialViewResult OnPostSearch()
    {
        var members = _memberService.SearchMembers(SearchCondition);
        Response.Headers.Add("X-total-records-count", (members.FirstOrDefault()?.TotalRecordsCount ?? 0).ToString());
        Response.Headers.Add("X-last-seq", members.Max(x => x?.Seq)?.ToString());
        return Partial("MemberList", members);
    }

    /// <summary>
    /// GetDetail（POST）リクエストハンドラー
    /// </summary>
    public PartialViewResult OnPostGetDetail([FromForm] string? detailKey)
    {
        ArgumentNullException.ThrowIfNull(detailKey);
        var member = _memberService.GetMember(detailKey) ?? throw new InvalidOperationException(detailKey.ToString());
        member.DepartmentListItems = DepartmentListItems.ToList();
        return Partial("MemberDetail", member);
    }

    /// <summary>
    /// GetBlankDetail（POST）リクエストハンドラー
    /// </summary>
    public PartialViewResult OnPostGetBlankDetail()
    {
        var member = new Member
        {
            MemberCode = "（新規）",
            DepartmentListItems = DepartmentListItems.ToList()
        };
        return Partial("MemberDetail", member);
    }

    /// <summary>
    /// SaveDetail（POST）リクエストハンドラー
    /// </summary>
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

    /// <summary>
    /// DownloadExcel（POST）リクエストハンドラー
    /// </summary>
    public FileContentResult OnPostDownloadExcel()
    {
        var bytes = _memberService.DownloadMembers(SearchCondition);
        Response.Headers.Add("X-download-file-name", $"Members_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        return File(bytes, CONTENT_TYPE_XLSX);
    }

    /// <summary>
    /// SuggestMemberCode（POST）リクエストハンドラー
    /// </summary>
    /// <param name="memberCodePart"></param>
    /// <returns></returns>
    public JsonResult OnPostSuggestMemberCode(string? memberCodePart)
    {
        ArgumentNullException.ThrowIfNull(memberCodePart);
        var list = _memberService.SuggestMemberCode(memberCodePart);
        return new JsonResult(list);
    }
}
