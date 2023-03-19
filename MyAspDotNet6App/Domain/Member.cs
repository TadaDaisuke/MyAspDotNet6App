namespace MyAspDotNet6App.Domain
{
    public class Member
    {
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string FullNameKana { get; set; }
        public string FullNameKanji { get; set; }
        public string MailAddress { get; set; }
        public DateOnly? JoinedDate { get; set; }

        public Member(
            string memberCode,
            string fullName,
            string fullNameKana,
            string fullNameKanji,
            string mailAddress,
            DateOnly? joinedDate)
        {
            MemberCode = memberCode;
            FullName = fullName;
            FullNameKana = fullNameKana;
            FullNameKanji = fullNameKanji;
            MailAddress = mailAddress;
            JoinedDate = joinedDate;
        }
    }
}
