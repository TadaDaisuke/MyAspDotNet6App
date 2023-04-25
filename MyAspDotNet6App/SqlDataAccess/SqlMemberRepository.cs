using Microsoft.Data.SqlClient;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.SqlDataAccess.Common;
using System.Data;

namespace MyAspDotNet6App.SqlDataAccess;

public class SqlMemberRepository : IMemberRepository
{
    private readonly MyAppContext _context;

    public SqlMemberRepository(MyAppContext context)
    {
        _context = context;
    }

    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition? memberSearchCondition)
    {
        var cmd = new SqlCommand("sp_search_members") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_name_part", SqlDbType.NVarChar, memberSearchCondition?.MemberNamePart)
            .AddParameter("@joined_date_from", SqlDbType.Date, memberSearchCondition?.JoinedDateFrom)
            .AddParameter("@joined_date_to", SqlDbType.Date, memberSearchCondition?.JoinedDateTo)
            .AddParameter("@sort_item", SqlDbType.NVarChar, memberSearchCondition?.SortItem)
            .AddParameter("@sort_type", SqlDbType.NVarChar, memberSearchCondition?.SortType)
            .AddParameter("@offset_rows", SqlDbType.Int, memberSearchCondition?.OffsetRows ?? 0)
            .AddParameter("@fetch_rows", SqlDbType.Int, _context.FETCH_ROW_SIZE);
        return _context.GetRowList(cmd)
            .Select(row =>
                new MemberListRow(
                    member: CreateMember(row),
                    seq: row["seq"].ToInt(),
                    totalRecordsCount: row["total_records_count"].ToInt()))
            .ToList();
    }

    public Member? GetMember(string memberCode)
    {
        var cmd = new SqlCommand("sp_get_member") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_code", SqlDbType.NVarChar, memberCode);
        return _context.GetRowList(cmd)
            .Select(row => CreateMember(row))
            .FirstOrDefault();
    }

    private Member CreateMember(Dictionary<string, string?> row)
        => new Member()
        {
            MemberCode = row["member_code"],
            GivenName = row["given_name"],
            FamilyName = row["family_name"],
            GivenNameKanji = row["given_name_kanji"],
            FamilyNameKanji = row["family_name_kanji"],
            GivenNameKana = row["given_name_kana"],
            FamilyNameKana = row["family_name_kana"],
            MailAddress = row["mail_address"],
            JoinedDate = row["joined_date"].ToNullableDateTime(DEFAULT_DATEONLY_FORMAT),
        };

    public void SaveMember(Member member)
    {
        var cmd = new SqlCommand("sp_merge_member") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_code", SqlDbType.NVarChar, member.MemberCode)
            .AddParameter("@given_name", SqlDbType.NVarChar, member.GivenName)
            .AddParameter("@family_name", SqlDbType.NVarChar, member.FamilyName)
            .AddParameter("@given_name_kana", SqlDbType.NVarChar, member.GivenNameKana)
            .AddParameter("@family_name_kana", SqlDbType.NVarChar, member.FamilyNameKana)
            .AddParameter("@given_name_kanji", SqlDbType.NVarChar, member.GivenNameKanji)
            .AddParameter("@family_name_kanji", SqlDbType.NVarChar, member.FamilyNameKanji)
            .AddParameter("@mail_address", SqlDbType.NVarChar, member.MailAddress)
            .AddParameter("@joined_date", SqlDbType.Date, member.JoinedDate)
            .AddOutputParameter("@error_message", SqlDbType.NVarChar, 4000);
        var errorMessage = _context.ExecuteSql(cmd).Parameters["@error_message"].Value.ToString();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new Exception(errorMessage);
        }
    }
}
