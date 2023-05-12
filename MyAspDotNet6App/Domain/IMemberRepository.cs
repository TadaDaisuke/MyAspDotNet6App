namespace MyAspDotNet6App.Domain;

public interface IMemberRepository
{
    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition searchCondition);

    public Member? GetMember(string memberCode);

    public void SaveMember(Member member);

    public byte[] CreateExcelBytes(MemberSearchCondition searchCondition, string sheetName);
}
