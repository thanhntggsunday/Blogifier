using Blogifier.Core.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.AdoNet.SQLServer
{
    public class DataAccess : BaseClass
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public DataAccess(string connectionStringName)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // thư mục chứa file json
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var section = config.GetSection("Blogifier");
            var connectionString = section.GetValue<string>(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public DataAccess()
        {
            var connectionString = ConnectionStringBuilder.ConnectionString;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public List<T> GetAllItems<T>(string sqlQuery, CommandType commandType, Func<SqlDataReader, T> mapper)
        {
            var items = new List<T>();

            using (var cmd = new SqlCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(mapper(reader));
                    }
                }
            }

            return items;
        }

        public List<T> GetItems<T>(string sqlQuery, List<SqlParameter> parameters, CommandType commandType, Func<SqlDataReader, T> mapper)
        {
            var items = new List<T>();

            using (var cmd = new SqlCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(mapper(reader));
                    }
                }
            }

            return items;
        }

        public int ExecuteNonQuery(string sqlQuery, List<SqlParameter> parameters, CommandType commandType)
        {
            using (var cmd = new SqlCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sqlQuery, List<SqlParameter> parameters, CommandType commandType)
        {
            using (var cmd = new SqlCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteScalar();
            }
        }

        public DataTable LoadDataTable(string sqlQuery, List<SqlParameter> parameters, CommandType commandType)
        {
            var dt = new DataTable();

            using (var cmd = new SqlCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }

            return dt;
        }

        private void BulkInsert(DataTable data, string tableName)
        {
            using (var bulkCopy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.Default, _transaction))
            {
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(data);
            }
        }

        public List<string> GetColumnNamesList(string tableName)
        {
            var result = new List<string>();

            var sql = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";

            using (var cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(rdr.GetString(0));
                    }
                }
            }

            return result;
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }

        private void CloseConnection()
        {
            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
            _transaction = null;
            _connection = null;
        }

        public override void Dispose()
        {
            CloseConnection();
            base.Dispose();
        }
    }
}
