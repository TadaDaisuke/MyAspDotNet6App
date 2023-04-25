using System.ComponentModel.DataAnnotations;

namespace MyAspDotNet6App.Domain;

public class DepartmentSearchCondition
{
    public int OffsetRows { get; set; } = 0;

    public string? SortItem { get; set; }

    public string? SortType { get; set; }

    [Display(Name = "部署名の一部")]
    public string? DepartmentNamePart { get; set; }
}
