using System.ComponentModel.DataAnnotations;

namespace MyAspDotNet6App.Domain;

public class Department
{
    [Display(Name = "部署コード")]
    [Required(AllowEmptyStrings = false)]
    public string? DepartmentCode { get; set; }

    [Display(Name = "部署名")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "部署名は入力必須です")]
    [RegularExpression(@"^\S+$", ErrorMessage = "空白文字は使用できません")]
    public string? DepartmentName { get; set; }
}
