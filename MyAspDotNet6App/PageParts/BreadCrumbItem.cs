namespace MyAspDotNet6App.PageParts;

public class BreadCrumbItem
{
    public string Url { get; set; }
    public string Title { get; set; }

    public BreadCrumbItem(string url, string title)
    {
        Url = url;
        Title = title;
    }
}
