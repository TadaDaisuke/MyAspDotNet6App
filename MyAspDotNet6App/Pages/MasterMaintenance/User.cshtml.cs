using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAspDotNet6App.Domain;

namespace MyAspDotNet6App.Pages.MasterMaintenance
{
    public class UserModel : PageModel
    {
        private readonly IUserService _userService;

        public UserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public SearchCondition SearchCondition { get; set; }

        public void OnGet()
        {
        }

        public PartialViewResult OnPostGetList()
        {
            return Partial("UserList", _userService.GetUsers(SearchCondition));
        }
    }

    public class SearchCondition
    {
        public string? UserNamePart { get; set; }
    }
}
