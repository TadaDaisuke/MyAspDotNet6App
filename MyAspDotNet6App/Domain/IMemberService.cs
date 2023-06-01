namespace MyAspDotNet6App.Domain;

public interface IMemberService
{
    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition searchCondition);

    public Member? GetMember(string memberCode);

    public void SaveMember(Member member);

    public byte[] DownloadMembers(MemberSearchCondition searchCondition);

    public IEnumerable<string> SuggestMemberCode(string memberCodePart);
}
