using System.ComponentModel.DataAnnotations;

namespace MyAspDotNet6App.Domain;

public class MemberSearchCondition
{
    public int OffsetRows { get; set; } = 0;

    public string? SortItem { get; set; }

    public string? SortType { get; set; }

    [Display(Name = "氏名の一部")]
    public string? MemberNamePart { get; set; }

    [Display(Name = "メンバーコード")]
    [StringLength(8)]
    public string? MemberCode { get; set; }

    [Display(Name = "着任日 From")]
    public DateTime? JoinedDateFrom { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない

    [Display(Name = "着任日 To")]
    public DateTime? JoinedDateTo { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない

    [Display(Name = "所属部署")]
    public string? DepartmentCode { get; set; }

    [Display(Name = "離任者を含む")]
    public bool HasTerminatedMembers { get; set; } = true;

    [Display(Name = "メールアドレスのドメイン")]
    public string? EmailDomain { get; set; }
}
