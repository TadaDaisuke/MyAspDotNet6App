namespace MyAspDotNet6App.PageParts;

/// <summary>
/// パンくずリストアイテム
/// </summary>
public class BreadCrumbItem
{
    /// <summary>
    /// URL
    /// </summary>
    public string Url { get; private set; }

    /// <summary>
    /// タイトル
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="title">タイトル</param>
    public BreadCrumbItem(string url, string title)
    {
        Url = url;
        Title = title;
    }
}
