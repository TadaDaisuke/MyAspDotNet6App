using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace MyAspDotNet6App.SqlDataAccess.Common
{
    public static class ExtensionMethods
    {
        public static SqlCommand ToSqlCommand(this StringBuilder sb)
        {
            return new SqlCommand(sb.ToString());
        }

        public static SqlCommand SetTimeOut(this SqlCommand cmd, int commandTimeout)
        {
            cmd.CommandTimeout = commandTimeout;
            return cmd;
        }

        public static SqlCommand AddParameter(this SqlCommand cmd, string parameterName, SqlDbType sqlDbType, object? value)
        {
            cmd.Parameters.Add(parameterName, sqlDbType).Value = value ?? DBNull.Value;
            return cmd;
        }

        public static SqlCommand AddParameter(this SqlCommand cmd, string parameterName, SqlDbType sqlDbType, int size, object? value)
        {
            cmd.Parameters.Add(parameterName, sqlDbType, size).Value = value ?? DBNull.Value;
            return cmd;
        }

        public static SqlCommand AddOutputParameter(this SqlCommand cmd, string parameterName, SqlDbType sqlDbType, int? size = null)
        {
            if (size == null)
            {
                cmd.Parameters.Add(parameterName, sqlDbType);
            }
            else
            {
                cmd.Parameters.Add(parameterName, sqlDbType, (int)size);
            }
            cmd.Parameters[parameterName].Direction = ParameterDirection.Output;
            return cmd;
        }
    }
}
