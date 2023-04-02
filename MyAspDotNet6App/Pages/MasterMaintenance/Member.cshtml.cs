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
        public MemberSearchCondition SearchCondition { get; set; } = new MemberSearchCondition();

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

        public PartialViewResult OnPostGetMemberDetail([FromForm] string? memberCode)
        {
            if (memberCode == null)
            {
                throw new ArgumentNullException(nameof(memberCode));
            }
            return Partial("MemberDetail", _memberService.GetMember(memberCode));
        }

        public ContentResult OnPostSaveMember([FromForm] Member? memberToSave)
        {
            if (memberToSave == null)
            {
                throw new ArgumentException(nameof(memberToSave));
            }
            if (!ModelState.IsValid)
            {
                throw new InvalidDataException(nameof(memberToSave));
            }
            _memberService.SaveMember(memberToSave);
            return Content("更新しました");
        }
    }
}
