namespace MyAspDotNet6App.PageParts;

/// <summary>
/// 一覧表の列情報
/// </summary>
public class ListColumn
{
    /// <summary>
    /// 列名
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 並び替え項目名
    /// </summary>
    public string? SortItem { get; private set; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="name">列名</param>
    /// <param name="sortItem">並び替え項目名</param>
    public ListColumn(string name, string? sortItem = null)
    {
        Name = name;
        SortItem = sortItem;
    }
}
