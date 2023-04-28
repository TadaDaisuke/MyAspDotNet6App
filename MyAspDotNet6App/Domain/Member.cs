using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyAspDotNet6App.Domain;

public class Member
{
    [Display(Name = "メンバーコード")]
    [Required(AllowEmptyStrings = false)]
    public string? MemberCode { get; set; }

    [Display(Name = "英字名")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "英字名は入力必須です")]
    [RegularExpression("^[A-Za-z]+$", ErrorMessage = "半角英字で入力してください")]
    public string? GivenName { get; set; }

    [Display(Name = "英字姓")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "英字姓は入力必須です")]
    [RegularExpression("^[A-Za-z]+$", ErrorMessage = "半角英字で入力してください")]
    public string? FamilyName { get; set; }

    [Display(Name = "漢字名")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "漢字名は入力必須です")]
    public string? GivenNameKanji { get; set; }

    [Display(Name = "漢字姓")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "漢字姓は入力必須です")]
    public string? FamilyNameKanji { get; set; }

    [Display(Name = "カナ名")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "カナ名は入力必須です")]
    [RegularExpression("^[ァ-ヴー]+$", ErrorMessage = "全角カタカナで入力してください")]
    public string? GivenNameKana { get; set; }

    [Display(Name = "カナ姓")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "カナ姓は入力必須です")]
    [RegularExpression("^[ァ-ヴー]+$", ErrorMessage = "全角カタカナで入力してください")]
    public string? FamilyNameKana { get; set; }

    [Display(Name = "メールアドレス")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "メールアドレスは入力必須です")]
    [EmailAddress(ErrorMessage = "正しいメールアドレスを入力してください")]
    public string? MailAddress { get; set; }

    [Display(Name = "着任日")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "着任日は入力必須です")]
    public DateTime? JoinedDate { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない

    [Display(Name = "所属部署")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "所属部署は選択必須です")]
    public string? DepartmentCode { get; set; }

    public string? DepartmentName { get; set; }

    public IEnumerable<SelectListItem>? DepartmentListItems { get; set; }

    public string FullName => $"{GivenName} {FamilyName}";

    public string FullNameKana => $"{FamilyNameKana} {GivenNameKana}";

    public string FullNameKanji => $"{FamilyNameKanji} {GivenNameKanji}";
}
