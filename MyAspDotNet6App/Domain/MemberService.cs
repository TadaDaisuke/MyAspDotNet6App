using MyAspDotNet6App.Utilities;

namespace MyAspDotNet6App.Domain;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IExcelCreator _excelCreator;

    public MemberService(IMemberRepository memberRepository, IExcelCreator excelCreator)
    {
        _memberRepository = memberRepository;
        _excelCreator = excelCreator;
    }

    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition? condition)
        => _memberRepository.SearchMembers(condition);

    public Member? GetMember(string memberCode)
        => _memberRepository.GetMember(memberCode);

    public void SaveMember(Member member)
        => _memberRepository.SaveMember(member);

    public byte[] DownloadMembers(MemberSearchCondition condition)
        => _excelCreator.CreateFileBytes(_memberRepository.GetDownloadCommand(condition), "Members");
}
