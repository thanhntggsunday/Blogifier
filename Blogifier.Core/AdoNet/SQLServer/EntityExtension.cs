using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Blogifier.Core.AdoNet.SQLServer
{
    public static class EntityExtension
    {
        public static SqlCommand GenerateInsertCommand<T>(this T entity, string tableName)
        {
            var type = typeof(T);
           
            var properties = type.GetProperties()
                .Where(p =>
                    p.Name.ToLower() != "id" &&
                    (!typeof(IEnumerable).IsAssignableFrom(p.PropertyType) || p.PropertyType == typeof(string)) &&
                    (!p.PropertyType.IsClass || p.PropertyType == typeof(string)))
                .ToList();

            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var paramNames = string.Join(", ", properties.Select(p => "@" + p.Name));
            var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({paramNames});";

            var command = new SqlCommand(sql);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(entity) ?? DBNull.Value;
                command.Parameters.AddWithValue("@" + prop.Name, value);
            }

            return command;
        }

        public static SqlCommand GenerateUpdateCommand<T>(this T entity, string tableName, List<string> lstCols)
        {
            var type = typeof(T);
            
            var properties = type.GetProperties()
                .Where(
                    p =>
                    p.Name.ToLower() != "id" &&
                    (!typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType) || p.PropertyType == typeof(string)) &&
                    (!p.PropertyType.IsClass || p.PropertyType == typeof(string)) && 
                    lstCols.Contains(p.Name, StringComparer.OrdinalIgnoreCase)
                )
                .ToList();

            var idProperty = type.GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property");

            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

            var command = new SqlCommand(sql);

            foreach (var prop in properties)
            {
                object value = prop.GetValue(entity) ?? DBNull.Value;
                command.Parameters.AddWithValue("@" + prop.Name, value);
            }

            object idValue = idProperty.GetValue(entity);
            command.Parameters.AddWithValue("@Id", idValue ?? throw new InvalidOperationException("Id cannot be null"));

            return command;
        }

        public static SqlCommand GenerateDeleteCommand<T>(this T entity, string tableName)
        {
            var type = typeof(T);
           
            var idProperty = type.GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property");

            var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
            var command = new SqlCommand(sql);

            object idValue = idProperty.GetValue(entity);
            command.Parameters.AddWithValue("@Id", idValue ?? throw new InvalidOperationException("Id cannot be null"));

            return command;
        }

        public static SqlCommand GenerateGetByIdCommand<T>(this T entity, string tableName)
        {
            var type = typeof(T);

            var idProperty = type.GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property");

            var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";
            var command = new SqlCommand(sql);

            object idValue = idProperty.GetValue(entity);
            command.Parameters.AddWithValue("@Id", idValue ?? throw new InvalidOperationException("Id cannot be null"));

            return command;
        }

    }
}