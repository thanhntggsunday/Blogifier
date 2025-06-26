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
    public interface IDataAccess
    {
        T GetById<T>(SqlCommand cmd, Func<SqlDataReader, T> mapper);
        List<T> GetAll<T>(string sqlQuery, CommandType commandType, Func<SqlDataReader, T> mapper);
        List<T> Find<T>(SqlCommand cmd, Func<SqlDataReader, T> mapper);
        int ExecuteNonQuery(SqlCommand cmd);
        object ExecuteScalar(SqlCommand cmd);
        DataTable LoadDataTable(SqlCommand cmd);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void Dispose();
        bool Disposed { get; set; }
    }

    public class DataAccess : BaseClass, IDataAccess
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public DataAccess(string connectionStringName)
        {
            var connectionString = ConnectionStringBuilder.GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public DataAccess()
        {
            var connectionString = ApplicationSettings.ConnectionString;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public T GetById<T>(SqlCommand cmd, Func<SqlDataReader, T> mapper)
        {
            var items = new List<T>();
            cmd.Connection = _connection;
            cmd.Transaction = _transaction;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    items.Add(mapper(reader));
                }
            }

            return items[0];
        }

        public List<T> GetAll<T>(string sqlQuery, CommandType commandType, Func<SqlDataReader, T> mapper)
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

        public List<T> Find<T>(SqlCommand cmd, Func<SqlDataReader, T> mapper)
        {
            var items = new List<T>();
            cmd.Connection = _connection;
            cmd.Transaction = _transaction;

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    items.Add(mapper(reader));
                }
            }

            return items;
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            cmd.Connection = _connection;
            cmd.Transaction = _transaction;

            return cmd.ExecuteNonQuery();
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            cmd.Connection = _connection;
            cmd.Transaction = _transaction;

            return cmd.ExecuteScalar();
        }

        public DataTable LoadDataTable(SqlCommand cmd)
        {
            var dt = new DataTable();
            cmd.Connection = _connection;
            cmd.Transaction = _transaction;

            using (var reader = cmd.ExecuteReader())
            {
                dt.Load(reader);
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
