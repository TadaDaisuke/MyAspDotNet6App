using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyAspDotNet6App.Pages;

/// <summary>
/// Indexページモデル
/// </summary>
public class IndexModel : PageModel
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<IndexModel> _logger;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="logger">ロガー</param>
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// GETリクエストハンドラー
    /// </summary>
    public void OnGet()
    {
    }
}