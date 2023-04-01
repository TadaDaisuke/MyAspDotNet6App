namespace MyAspDotNet6App.Domain
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository) => _memberRepository = memberRepository;

        public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition? condition) => _memberRepository.SearchMembers(condition);

        public Member? GetMember(string memberCode) => _memberRepository.GetMember(memberCode);

        public void SaveMember(Member member) => _memberRepository.SaveMember(member);
    }
}
