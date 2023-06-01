namespace MyAspDotNet6App.PageParts;

public class RadioItem
{
    public string Label { get; set; }
    public string Value { get; set; }
    public bool IsChecked { get; set; }

    public RadioItem(string label, string value, bool isChecked = false)
    {
        Label = label;
        Value = value;
        IsChecked = isChecked;
    }
}
