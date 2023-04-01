using System.ComponentModel.DataAnnotations;

namespace MyAspDotNet6App.Domain
{
    public class Member
    {
        [Display(Name = "メンバーコード")]
        public string? MemberCode { get; set; }

        [Display(Name = "英字名")]
        public string? GivenName { get; set; }

        [Display(Name = "英字姓")]
        public string? FamilyName { get; set; }

        [Display(Name = "漢字名")]
        public string? GivenNameKanji { get; set; }

        [Display(Name = "漢字姓")]
        public string? FamilyNameKanji { get; set; }

        [Display(Name = "カナ名")]
        public string? GivenNameKana { get; set; }

        [Display(Name = "カナ姓")]
        public string? FamilyNameKana { get; set; }

        [Display(Name = "メールアドレス")]
        public string? MailAddress { get; set; }

        [Display(Name = "着任日")]
        public DateTime? JoinedDate { get; set; } // ASP.NET6ではまだBindPropertyにDateOnly型は使えない

        public string FullName => $"{GivenName} {FamilyName}";

        public string FullNameKana => $"{FamilyNameKana} {GivenNameKana}";

        public string FullNameKanji => $"{FamilyNameKanji} {GivenNameKanji}";
    }
}
