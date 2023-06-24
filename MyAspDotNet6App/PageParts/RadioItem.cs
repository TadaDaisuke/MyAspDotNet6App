namespace MyAspDotNet6App.PageParts;

/// <summary>
/// ラジオボタンアイテム
/// </summary>
public class RadioItem
{
    /// <summary>
    /// ラベル
    /// </summary>
    public string Label { get; private set; }

    /// <summary>
    /// 値
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// チェック有無
    /// </summary>
    public bool IsChecked { get; private set; }

    /// <summary>
    /// コンストラクター
    /// </summary>
    /// <param name="label">ラベル</param>
    /// <param name="value">値</param>
    /// <param name="isChecked">[オプション] true: チェックあり, false: チェックなし（既定値 = false）。</param>
    public RadioItem(string label, string value, bool isChecked = false)
    {
        Label = label;
        Value = value;
        IsChecked = isChecked;
    }
}
