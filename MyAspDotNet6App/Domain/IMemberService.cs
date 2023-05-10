namespace MyAspDotNet6App.Domain;

public interface IMemberService
{
    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition? condition);

    public Member? GetMember(string memberCode);

    public void SaveMember(Member member);

    public byte[] DownloadMembers(MemberSearchCondition condition);
}
