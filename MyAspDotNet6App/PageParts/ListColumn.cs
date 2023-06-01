namespace MyAspDotNet6App.PageParts;

public class ListColumn
{
    public string Name { get; set; }
    public string? SortItem { get; set; }

    public ListColumn(string name, string? sortItem = null)
    {
        Name = name;
        SortItem = sortItem;
    }
}
