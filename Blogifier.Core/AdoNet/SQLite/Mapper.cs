using Blogifier.Core.Common;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blogifier.Core.AdoNet.SQLite
{
    public static class Mapper
    {
        public static Func<SqliteDataReader, T> CreateMapper<T>() where T : new()
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanWrite)
                                 .ToList();

            return reader =>
            {
                var obj = new T();

                foreach (var prop in props)
                {
                    try
                    {
                        var columnName = prop.Name;

                        if (!ColumnExists(reader, columnName))
                            continue;

                        var val = reader[columnName];
                        if (val == DBNull.Value)
                            continue;

                        var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        // Special handling for enums
                        if (targetType.IsEnum)
                        {
                            prop.SetValue(obj, Enum.Parse(targetType, val.ToString()));
                        }
                        else
                        {
                            prop.SetValue(obj, Convert.ChangeType(val, targetType));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Error in CreateMapper: {ex.Message}");
                        Logger.LogError(ex.ToString());
                    }
                }

                return obj;
            };
        }

        private static bool ColumnExists(SqliteDataReader reader, string columnName)
        {
            try
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in ColumnExists: " + ex.Message);
                return false;
            }
        }
    }
}
