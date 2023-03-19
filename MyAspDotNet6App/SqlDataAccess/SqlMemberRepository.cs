using MyAspDotNet6App.Common;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.SqlDataAccess.Common;
using System.Text;
using System.Data;

namespace MyAspDotNet6App.SqlDataAccess
{
    public class SqlMemberRepository : IMemberRepository
    {
        private readonly MyAppContext _context;

        public SqlMemberRepository(MyAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> SearchMembers(MemberSearchCondition? memberSearchCondition)
        {
            var cmd = new StringBuilder()
                .AppendLine("SELECT member_code")
                .AppendLine("    ,given_name")
                .AppendLine("    ,family_name")
                .AppendLine("    ,given_name_kana")
                .AppendLine("    ,family_name_kana")
                .AppendLine("    ,given_name_kanji")
                .AppendLine("    ,family_name_kanji")
                .AppendLine("    ,mail_address")
                .AppendLine("    ,joined_date")
                .AppendLine("FROM member")
                .AppendLine("WHERE (")
                .AppendLine("        @member_name_part IS NULL")
                .AppendLine("        OR given_name LIKE N'%' + @member_name_part + N'%'")
                .AppendLine("        OR family_name LIKE N'%' + @member_name_part + N'%'")
                .AppendLine("        OR given_name_kana LIKE N'%' + @member_name_part + N'%'")
                .AppendLine("        OR family_name_kana LIKE N'%' + @member_name_part + N'%'")
                .AppendLine("        OR given_name_kanji LIKE N'%' + @member_name_part + N'%'")
                .AppendLine("        OR family_name_kanji LIKE N'%' + @member_name_part + N'%'")
                .AppendLine("        )")
                .AppendLine("    AND (")
                .AppendLine("        @joined_date_from IS NULL")
                .AppendLine("        OR @joined_date_from <= joined_date")
                .AppendLine("        )")
                .AppendLine("    AND (")
                .AppendLine("        @joined_date_to IS NULL")
                .AppendLine("        OR joined_date <= @joined_date_to")
                .AppendLine("        )")
                .ToSqlCommand()
                .AddParameter("@member_name_part", SqlDbType.NVarChar, memberSearchCondition?.MemberNamePart)
                .AddParameter("@joined_date_from", SqlDbType.Date, memberSearchCondition?.JoinedDateFrom)
                .AddParameter("@joined_date_to", SqlDbType.Date, memberSearchCondition?.JoinedDateTo);
            return _context.GetRowList(cmd)
                .Select(row =>
                    new Member(
                        memberCode: row["member_code"] ?? string.Empty,
                        fullName: $"{row["family_name"]} {row["given_name"]}",
                        fullNameKana: $"{row["family_name_kana"]} {row["given_name_kana"]}",
                        fullNameKanji: $"{row["family_name_kanji"]} {row["given_name_kanji"]}",
                        mailAddress: row["mail_address"] ?? string.Empty,
                        joinedDate: row["joined_date"].ToNullableDateOnly()))
                .ToList();
        }
    }
}
