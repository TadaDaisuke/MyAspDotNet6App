namespace MyAspDotNet6App.Domain;

/// <summary>
/// メンバーサービス実装クラス
/// </summary>
public class MemberService : IMemberService
{
    /// <summary>
    /// メンバーリポジトリー
    /// </summary>
    private readonly IMemberRepository _memberRepository;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="memberRepository">メンバーリポジトリー</param>
    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    /// <inheritdoc/>
    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition searchCondition)
        => _memberRepository.SearchMembers(searchCondition);

    /// <inheritdoc/>
    public Member? GetMember(string memberCode)
        => _memberRepository.GetMember(memberCode);

    /// <inheritdoc/>
    public void SaveMember(Member member)
        => _memberRepository.SaveMember(member);

    /// <inheritdoc/>
    public byte[] DownloadMembers(MemberSearchCondition searchCondition)
        => _memberRepository.CreateExcelBytes(searchCondition, "Members");

    /// <inheritdoc/>
    public IEnumerable<string> SuggestMemberCode(string memberCodePart)
        => _memberRepository.SuggestMemberCode(memberCodePart);
}
