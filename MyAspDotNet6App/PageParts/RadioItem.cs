namespace MyAspDotNet6App.PageParts;

public class RadioItem
{
    public string Id { get; set; }
    public string Label { get; set; }
    public string Value { get; set; }
    public bool IsChecked { get; set; }

    public RadioItem(string id, string label, string value, bool isChecked = false)
    {
        Id = id;
        Label = label;
        Value = value;
        IsChecked = isChecked;
    }
}
