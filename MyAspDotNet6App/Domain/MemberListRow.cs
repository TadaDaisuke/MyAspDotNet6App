namespace MyAspDotNet6App.Domain;

/// <summary>
/// メンバー一覧の行
/// </summary>
public class MemberListRow
{
    /// <summary>
    /// メンバー
    /// </summary>
    public Member Member { get; private set; }

    /// <summary>
    /// 行番号
    /// </summary>
    public int Seq { get; private set; }

    /// <summary>
    /// 総行数
    /// </summary>
    public int TotalRecordsCount { get; private set; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="member">メンバー</param>
    /// <param name="seq">行番号</param>
    /// <param name="totalRecordsCount">総行数</param>
    public MemberListRow(
        Member member,
        int seq,
        int totalRecordsCount)
    {
        Member = member;
        Seq = seq;
        TotalRecordsCount = totalRecordsCount;
    }
}
