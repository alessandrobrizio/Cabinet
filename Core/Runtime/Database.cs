using Mono.Data.Sqlite;
using UnityEngine;

namespace AlessandroBrizio.Cabinet.Core
{
    public class Database : System.IDisposable
    {
        public bool debugMode;
        private bool _disposedValue;
        private readonly string _dataSource;
        private readonly SqliteConnection _connection;
        private readonly SqliteCommand _command;

        public Database(string dataSource)
        {
            _dataSource = dataSource;
            string connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = _dataSource,
                Version = 3
            }.ToString();
            _connection = new SqliteConnection(connectionString);
            _command = _connection.CreateCommand();
        }

        public TTable Get<TTable>() where TTable : DatabaseTable, new()
        {
            TTable table = new TTable {database = this};
            table.CreateTable();
            return table;
        }

        public int ExecuteNonQuery(string commandText)
        {
            DebugLog(commandText);
            try
            {
                _connection.Open();
                _command.CommandText = commandText;
                int changedRows = _command.ExecuteNonQuery();
                return changedRows;
            }
            finally
            {
                _connection.Close();
            }
        }

        public T ExecuteReader<T>(string commandText, System.Func<SqliteDataReader, T> readFunc)
        {
            DebugLog(commandText);
            try
            {
                _connection.Open();
                _command.CommandText = commandText;
                using (SqliteDataReader reader = _command.ExecuteReader())
                {
                    return readFunc(reader);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        private void DebugLog(string text)
        {
            if (debugMode)
            {
                Debug.Log(text);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();
                    _command.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}
