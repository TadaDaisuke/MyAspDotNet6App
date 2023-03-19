namespace MyAspDotNet6App.Domain
{
    public interface IMemberService
    {
        public IEnumerable<Member> SearchMembers(MemberSearchCondition? condition);
    }
}
