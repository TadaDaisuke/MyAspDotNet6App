﻿using Microsoft.Data.SqlClient;
using MyAspDotNet6App.Domain;
using MyAspDotNet6App.Utilities;
using System.Data;

namespace MyAspDotNet6App.MssqlDataAccess;

/// <summary>
/// メンバーリポジトリー実装クラス
/// </summary>
public class MssqlMemberRepository : IMemberRepository
{
    /// <summary>
    /// MyDatabaseアクセス関連のコンテキスト
    /// </summary>
    private readonly MyDatabaseContext _context;

    /// <summary>
    /// Excel生成ユーティリティー
    /// </summary>
    private readonly IExcelCreator _excelCreator;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="context">MyDatabaseアクセス関連のコンテキスト</param>
    /// <param name="excelCreator">Excel生成ユーティリティー</param>
    public MssqlMemberRepository(MyDatabaseContext context, IExcelCreator excelCreator)
    {
        _context = context;
        _excelCreator = excelCreator;
    }

    /// <inheritdoc/>
    public IEnumerable<MemberListRow> SearchMembers(MemberSearchCondition searchCondition)
    {
        var cmd = new SqlCommand("sp_search_members") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_name_part", SqlDbType.NVarChar, searchCondition.MemberNamePart)
            .AddParameter("@member_code", SqlDbType.NVarChar, searchCondition.MemberCode)
            .AddParameter("@joined_date_from", SqlDbType.Date, searchCondition.JoinedDateFrom)
            .AddParameter("@joined_date_to", SqlDbType.Date, searchCondition.JoinedDateTo)
            .AddParameter("@department_code", SqlDbType.NVarChar, searchCondition.DepartmentCode.OrNullIfWhiteSpace())
            .AddParameter("@has_terminated_members", SqlDbType.Bit, searchCondition.HasTerminatedMembers)
            .AddParameter("@email_domain", SqlDbType.NVarChar, searchCondition.EmailDomain)
            .AddParameter("@sort_item", SqlDbType.NVarChar, searchCondition.SortItem)
            .AddParameter("@sort_type", SqlDbType.NVarChar, searchCondition.SortType)
            .AddParameter("@offset_rows", SqlDbType.Int, searchCondition.OffsetRows)
            .AddParameter("@fetch_rows", SqlDbType.Int, _context.FETCH_ROW_SIZE);
        return _context.GetRowList(cmd)
            .Select(row =>
                new MemberListRow(
                    member: CreateMember(row),
                    seq: row["seq"].ToInt(),
                    totalRecordsCount: row["total_records_count"].ToInt()))
            .ToList();
    }

    /// <inheritdoc/>
    public Member? GetMember(string memberCode)
    {
        var cmd = new SqlCommand("sp_get_member") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_code", SqlDbType.NVarChar, memberCode);
        return _context.GetRowList(cmd)
            .Select(row => CreateMember(row))
            .FirstOrDefault();
    }

    /// <summary>
    /// 検索結果1行から、Memberオブジェクトを生成して返す。
    /// </summary>
    private static Member CreateMember(Dictionary<string, string?> row)
    {
        var member = new Member()
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
            TerminationDate = row["termination_date"].ToNullableDateTime(DEFAULT_DATEONLY_FORMAT),
        };
        if (row.ContainsKey("department_code"))
        {
            member.DepartmentCode = row["department_code"];
        }
        if (row.ContainsKey("department_name"))
        {
            member.DepartmentName = row["department_name"];
        }
        if (row.ContainsKey("note"))
        {
            member.Note = row["note"];
        }
        return member;
    }

    /// <inheritdoc/>
    public void SaveMember(Member member)
    {
        var cmd = new SqlCommand("sp_save_member") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_code", SqlDbType.NVarChar, member.MemberCode)
            .AddParameter("@given_name", SqlDbType.NVarChar, member.GivenName)
            .AddParameter("@family_name", SqlDbType.NVarChar, member.FamilyName)
            .AddParameter("@given_name_kana", SqlDbType.NVarChar, member.GivenNameKana)
            .AddParameter("@family_name_kana", SqlDbType.NVarChar, member.FamilyNameKana)
            .AddParameter("@given_name_kanji", SqlDbType.NVarChar, member.GivenNameKanji)
            .AddParameter("@family_name_kanji", SqlDbType.NVarChar, member.FamilyNameKanji)
            .AddParameter("@mail_address", SqlDbType.NVarChar, member.MailAddress)
            .AddParameter("@department_code", SqlDbType.NVarChar, member.DepartmentCode)
            .AddParameter("@joined_date", SqlDbType.Date, member.JoinedDate)
            .AddParameter("@termination_date", SqlDbType.Date, member.TerminationDate)
            .AddParameter("@note", SqlDbType.NVarChar, member.Note)
            .AddOutputParameter("@error_message", SqlDbType.NVarChar, 4000);
        var errorMessage = _context.ExecuteSql(cmd).Parameters["@error_message"].Value.ToString();
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new Exception(errorMessage);
        }
    }

    /// <inheritdoc/>
    public byte[] CreateExcelBytes(MemberSearchCondition searchCondition, string sheetName)
    {
        var cmd = new SqlCommand("sp_download_members") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_name_part", SqlDbType.NVarChar, searchCondition.MemberNamePart)
            .AddParameter("@member_code", SqlDbType.NVarChar, searchCondition.MemberCode)
            .AddParameter("@joined_date_from", SqlDbType.Date, searchCondition.JoinedDateFrom)
            .AddParameter("@joined_date_to", SqlDbType.Date, searchCondition.JoinedDateTo)
            .AddParameter("@department_code", SqlDbType.NVarChar, searchCondition.DepartmentCode.OrNullIfWhiteSpace())
            .AddParameter("@has_terminated_members", SqlDbType.Bit, searchCondition.HasTerminatedMembers)
            .AddParameter("@email_domain", SqlDbType.NVarChar, searchCondition.EmailDomain)
            .AddParameter("@sort_item", SqlDbType.NVarChar, searchCondition.SortItem)
            .AddParameter("@sort_type", SqlDbType.NVarChar, searchCondition.SortType);
        return _excelCreator.CreateFileBytes(cmd, sheetName);
    }

    /// <inheritdoc/>
    public IEnumerable<string> SuggestMemberCode(string memberCodePart)
    {
        var cmd = new SqlCommand("sp_suggest_member_code") { CommandType = CommandType.StoredProcedure }
            .AddParameter("@member_code_part", SqlDbType.NVarChar, memberCodePart);
        return _context.GetRowList(cmd).Select(row => row["member_code"] ?? string.Empty).ToList();
    }
}
