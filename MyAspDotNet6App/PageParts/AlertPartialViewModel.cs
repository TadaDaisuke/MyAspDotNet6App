namespace MyAspDotNet6App.PageParts;

/// <summary>
/// _AlertPartialビューモデル
/// </summary>
public class AlertPartialViewModel
{
    /// <summary>
    /// アラート種別
    /// </summary>
    public AlertType AlertType { get; private set; }

    /// <summary>
    /// アラートメッセージ
    /// </summary>
    public string AlertMessage { get; private set; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="alertType">アラート種別</param>
    /// <param name="alertMessage">アラートメッセージ</param>
    public AlertPartialViewModel(AlertType alertType, string alertMessage)
    {
        AlertType = alertType;
        AlertMessage = alertMessage;
    }
}
