using Microsoft.Data.SqlClient;
using System.Data;
using static MyAspDotNet6App.SqlDataAccess.Common.Constants;

namespace MyAspDotNet6App.SqlDataAccess
{
    public class MyAppContext
    {
        private string _connectionString { get; set; }

        public MyAppContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Dictionary<string, string?>> GetRowList(SqlCommand cmd, List<string>? fieldNames = null, List<SqlDbType>? fieldTypes = null)
        {
            var list = new List<Dictionary<string, string?>>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
                {
                    if (fieldNames != null)
                    {
                        for (var index = 0; index < reader.FieldCount; index++)
                        {
                            fieldNames.Add(reader.GetName(index));
                        }
                    }
                    if (fieldTypes != null)
                    {
                        for (var index = 0; index < reader.FieldCount; index++)
                        {
                            fieldTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType), reader.GetDataTypeName(index), true));
                        }
                    }
                    while (reader.Read())
                    {
                        var dict = new Dictionary<string, string?>(reader.FieldCount);
                        for (var index = 0; index < reader.FieldCount; index++)
                        {
                            var colName = reader.GetName(index);
                            var colValue = reader.GetValue(index);
                            dict.Add
                            (
                                string.IsNullOrWhiteSpace(colName) ? Guid.NewGuid().ToString() : colName,
                                (colValue == null || colValue == DBNull.Value)
                                    ? null
                                    : colValue is DateTime dateTimeValue
                                        ? dateTimeValue.ToString(reader.GetDataTypeName(index) == "date" ? DEFAULT_DATEONLY_FORMAT : DEFAULT_DATETIME_FORMAT)
                                        : colValue.ToString()
                            );
                        }
                        list.Add(dict);
                    }
                }
            }
            return list;
        }


    }
}
