namespace MyAspDotNet6App.Domain
{
    public class Member
    {
        public int Seq { get; set; }
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public string FullNameKana { get; set; }
        public string FullNameKanji { get; set; }
        public string MailAddress { get; set; }
        public DateOnly? JoinedDate { get; set; }
        public int TotalRecordsCount { get; set; }

        public Member(
            int seq,
            string memberCode,
            string fullName,
            string fullNameKana,
            string fullNameKanji,
            string mailAddress,
            DateOnly? joinedDate,
            int totalRecordsCount)
        {
            Seq = seq;
            MemberCode = memberCode;
            FullName = fullName;
            FullNameKana = fullNameKana;
            FullNameKanji = fullNameKanji;
            MailAddress = mailAddress;
            JoinedDate = joinedDate;
            TotalRecordsCount = totalRecordsCount;
        }
    }
}
