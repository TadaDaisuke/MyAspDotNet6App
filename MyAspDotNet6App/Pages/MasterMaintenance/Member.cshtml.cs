using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAspDotNet6App.Domain;

namespace MyAspDotNet6App.Pages.MasterMaintenance
{
    public class MemberModel : PageModel
    {
        private readonly IMemberService _memberService;

        public MemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
        public MemberSearchCondition? SearchCondition { get; set; }

        [BindProperty]
        public string MemberCode { get; set; } = string.Empty;

        [BindProperty]
        public Member? MemberToSave { get; set; }

        public void OnGet()
        {
        }

        public PartialViewResult OnPostSearchMember()
        {
            var members = _memberService.SearchMembers(SearchCondition);
            Response.Headers.Add("X-total-records-count", (members.FirstOrDefault()?.TotalRecordsCount ?? 0).ToString());
            Response.Headers.Add("X-last-seq", members.Max(x => x?.Seq)?.ToString());
            return Partial("MemberList", members);
        }

        public PartialViewResult OnPostGetMemberDetail()
        {
            return Partial("MemberDetail", _memberService.GetMember(MemberCode));
        }

        public ContentResult OnPostSaveMember()
        {
            if (MemberToSave == null)
            {
                throw new ArgumentException(nameof(MemberToSave));
            }
            _memberService.SaveMember(MemberToSave);
            return Content("çXêVÇµÇ‹ÇµÇΩ");
        }
    }
}
