using MyAspDotNet6App.Common;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.SqlDataAccess.Common;
using System.Data;
using Microsoft.Data.SqlClient;

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
            var cmd = new SqlCommand("sp_search_member") { CommandType = CommandType.StoredProcedure }
                .AddParameter("@member_name_part", SqlDbType.NVarChar, memberSearchCondition?.MemberNamePart)
                .AddParameter("@joined_date_from", SqlDbType.Date, memberSearchCondition?.JoinedDateFrom)
                .AddParameter("@joined_date_to", SqlDbType.Date, memberSearchCondition?.JoinedDateTo)
                .AddParameter("@offset_rows", SqlDbType.Int, 0)
                .AddParameter("@fetch_rows", SqlDbType.Int, 25);
            return _context.GetRowList(cmd)
                .Select(row =>
                    new Member(
                        seq: row["seq"].ToInt(),
                        memberCode: row["member_code"] ?? string.Empty,
                        fullName: $"{row["family_name"]} {row["given_name"]}",
                        fullNameKana: $"{row["family_name_kana"]} {row["given_name_kana"]}",
                        fullNameKanji: $"{row["family_name_kanji"]} {row["given_name_kanji"]}",
                        mailAddress: row["mail_address"] ?? string.Empty,
                        joinedDate: row["joined_date"].ToNullableDateOnly(),
                        totalRecordsCount: row["total_records_count"].ToInt()))
                .ToList();
        }
    }
}
