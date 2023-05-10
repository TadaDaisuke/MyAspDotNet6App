using Microsoft.Data.SqlClient;

namespace MyAspDotNet6App.Utilities;

public interface IExcelCreator
{
    public byte[] CreateFileBytes(SqlCommand cmd, string sheetName);
}
