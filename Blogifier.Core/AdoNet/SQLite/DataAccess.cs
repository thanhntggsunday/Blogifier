using Blogifier.Core.Common;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Blogifier.Core.AdoNet.SQLite
{
    public class DataAccess : BaseClass
    {
        private SqliteConnection _connection;
        private SqliteTransaction _transaction;

        public DataAccess(string connectionStringName)
        {
            var connectionString = ConnectionStringBuilder.GetConnectionString(connectionStringName);

            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        }

        public DataAccess()
        {
            var connectionString = ApplicationSettings.ConnectionString;
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        }

        public List<T> GetAllItems<T>(string sqlQuery, CommandType commandType, Func<SqliteDataReader, T> mapper)
        {
            var items = new List<T>();

            using (var cmd = new SqliteCommand(sqlQuery, _connection, _transaction))
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

        public List<T> GetItems<T>(string sqlQuery, List<SqliteParameter> parameters, CommandType commandType, Func<SqliteDataReader, T> mapper)
        {
            var items = new List<T>();

            using (var cmd = new SqliteCommand(sqlQuery, _connection, _transaction))
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

        public int ExecuteNonQuery(string sqlQuery, List<SqliteParameter> parameters, CommandType commandType)
        {
            using (var cmd = new SqliteCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sqlQuery, List<SqliteParameter> parameters, CommandType commandType)
        {
            using (var cmd = new SqliteCommand(sqlQuery, _connection, _transaction))
            {
                cmd.CommandType = commandType;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray());

                return cmd.ExecuteScalar();
            }
        }

        public DataTable LoadDataTable(string sqlQuery, List<SqliteParameter> parameters, CommandType commandType)
        {
            var dt = new DataTable();

            using (var cmd = new SqliteCommand(sqlQuery, _connection, _transaction))
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

        private void BulkInsert_NotSupported()
        {
            throw new NotSupportedException("BulkInsert is not supported in SQLite.");
        }

        public List<string> GetColumnNamesList(string tableName)
        {
            var result = new List<string>();
            var sql = $"PRAGMA table_info([{tableName}]);";

            using (var cmd = new SqliteCommand(sql, _connection))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(rdr.GetString(1)); // cột "name"
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
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        public override void Dispose()
        {
            CloseConnection();
            base.Dispose();
        }
    }
}
