using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyAspDotNet6App.Pages;

/// <summary>
/// Errorページモデル
/// </summary>
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<ErrorModel> _logger;

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="logger">ロガー</param>
    public ErrorModel(ILogger<ErrorModel> logger)
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
