using System.ComponentModel.DataAnnotations;

namespace MyAspDotNet6App.Domain
{
    public class MemberSearchCondition
    {
        [Display(Name = "氏名の一部")]
        public string? MemberNamePart { get; set; }

        [Display(Name = "登録日 From")]
        public DateTime? JoinedDateFrom { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない

        [Display(Name = "登録日 To")]
        public DateTime? JoinedDateTo { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない
    }
}
