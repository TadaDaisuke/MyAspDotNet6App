namespace MyAspDotNet6App.Domain;

/// <summary>
/// 部署一覧の行
/// </summary>
public class DepartmentListRow
{
    /// <summary>
    /// 部署
    /// </summary>
    public Department Department { get; private set; }

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
    /// <param name="department">部署</param>
    /// <param name="seq">行番号</param>
    /// <param name="totalRecordsCount">総行数</param>
    public DepartmentListRow(
        Department department,
        int seq,
        int totalRecordsCount)
    {
        Department = department;
        Seq = seq;
        TotalRecordsCount = totalRecordsCount;
    }
}
