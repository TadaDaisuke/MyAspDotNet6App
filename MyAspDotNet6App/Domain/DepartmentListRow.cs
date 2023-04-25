namespace MyAspDotNet6App.Domain;

public class DepartmentListRow
{
    public Department Department { get; set; }
    public int Seq { get; set; }
    public int TotalRecordsCount { get; set; }

    public DepartmentListRow(
        Department department,
        int seq,
        int totalRecordsCount)
    {
        Department = department;
        Seq = seq;
        TotalRecordsCount = totalRecordsCount;
    }
}
