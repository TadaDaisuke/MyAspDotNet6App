﻿namespace MyAspDotNet6App.Domain;

public interface IMemberRepository
{
    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition? condition);

    public Member? GetMember(string memberCode);

    public void SaveMember(Member member);
}
