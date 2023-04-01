namespace MyAspDotNet6App.Domain
{
    public class MemberListRow
    {
        public Member Member { get; set; }
        public int Seq { get; set; }
        public int TotalRecordsCount { get; set; }

        public MemberListRow(
            Member member,
            int seq,
            int totalRecordsCount)
        {
            Member = member;
            Seq = seq;
            TotalRecordsCount = totalRecordsCount;
        }
    }
}
