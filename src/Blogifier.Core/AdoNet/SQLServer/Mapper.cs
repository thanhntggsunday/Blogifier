using Blogifier.Core.Common;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.AdoNet.SQLServer
{
    public static class Mapper
    {
        public static Func<SqlDataReader, T> CreateMapper<T>() where T : new()
        {
            var props = typeof(T).GetProperties().Where(p => p.CanWrite).ToList();

            return reader =>
            {
                var obj = new T();

                foreach (var prop in props)
                {
                    try
                    {
                        var columnName = prop.Name;

                        if (!ColumnExists(reader, columnName)) { continue; }

                        var val = reader[columnName];

                        if (val == DBNull.Value) { continue; }

                        prop.SetValue(obj, Convert.ChangeType(val, prop.PropertyType));
                    }
                    catch (Exception ex)
                    {
                        // Optionally log or ignore conversion errors
                        AppLogger.LogError("Error CreateMapper: " + ex.Message);
                        AppLogger.LogError(ex.ToString());
                    }
                }

                return obj;
            };
        }

        private static bool ColumnExists(SqlDataReader reader, string columnName)
        {
            try
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex.ToString());

                return false;
            }
        }
    }
}
