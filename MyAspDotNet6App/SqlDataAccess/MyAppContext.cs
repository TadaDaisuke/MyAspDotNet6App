﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace MyAspDotNet6App.SqlDataAccess;

public class MyAppContext
{
    private readonly string _connectionString;

    public readonly int FETCH_ROW_SIZE = 50;

    public MyAppContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Dictionary<string, string?>> GetRowList(SqlCommand cmd, out DataTable schemaTable)
    {
        var list = new List<Dictionary<string, string?>>();
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            cmd.Connection = conn;
            using (var reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
            {
                schemaTable = reader.GetSchemaTable();
                var uniqueColNames = new List<string>();
                foreach (var colName in schemaTable.AsEnumerable().Select(row => row.Field<string>("ColumnName")))
                {
                    uniqueColNames.Add((string.IsNullOrWhiteSpace(colName) || uniqueColNames.Contains(colName)) ? Guid.NewGuid().ToString() : colName);
                }
                while (reader.Read())
                {
                    var dict = new Dictionary<string, string?>(reader.FieldCount);
                    for (var index = 0; index < reader.FieldCount; index++)
                    {
                        var colValue = reader.GetValue(index);
                        dict.Add
                        (
                            uniqueColNames[index],
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

    public List<Dictionary<string, string?>> GetRowList(SqlCommand cmd) => GetRowList(cmd, out _);

    public SqlCommand ExecuteSql(SqlCommand cmd)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }
        return cmd;
    }
}
