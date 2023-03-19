namespace MyAspDotNet6App.Domain
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IEnumerable<Member> SearchMembers(MemberSearchCondition? condition)
        {
            return _memberRepository.SearchMembers(condition);
        }
    }
}
