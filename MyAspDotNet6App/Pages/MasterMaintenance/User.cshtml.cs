using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAspDotNet6App.Domain;
using System.ComponentModel.DataAnnotations;

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
        public UserSearchCondition? SearchCondition { get; set; }

        public void OnGet()
        {
        }

        public PartialViewResult OnPostGetList()
        {
            return Partial("UserList", _userService.GetUsers(SearchCondition));
        }
    }

    public class UserSearchCondition
    {
        [Display(Name = "ユーザー名")]
        public string? UserNamePart { get; set; }

        [Display(Name = "入社日 From")]
        public DateTime? JoinedDateFrom { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない
    }
}
