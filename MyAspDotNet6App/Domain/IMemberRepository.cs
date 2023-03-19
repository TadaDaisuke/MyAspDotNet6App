namespace MyAspDotNet6App.Domain
{
    public interface IMemberRepository
    {
        public IEnumerable<Member> SearchMembers(MemberSearchCondition? condition);
    }
}
