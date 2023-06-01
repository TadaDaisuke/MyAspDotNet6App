namespace MyAspDotNet6App.Domain;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition searchCondition)
        => _memberRepository.SearchMembers(searchCondition);

    public Member? GetMember(string memberCode)
        => _memberRepository.GetMember(memberCode);

    public void SaveMember(Member member)
        => _memberRepository.SaveMember(member);

    public byte[] DownloadMembers(MemberSearchCondition searchCondition)
        => _memberRepository.CreateExcelBytes(searchCondition, "Members");

    public IEnumerable<string> SuggestMemberCode(string memberCodePart)
        => _memberRepository.SuggestMemberCode(memberCodePart);
}
