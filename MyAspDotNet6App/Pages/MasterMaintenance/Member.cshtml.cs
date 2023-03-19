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

        public void OnGet()
        {
        }

        public PartialViewResult OnPostSearchMember()
        {
            return Partial("MemberList", _memberService.SearchMembers(SearchCondition));
        }
    }
}
