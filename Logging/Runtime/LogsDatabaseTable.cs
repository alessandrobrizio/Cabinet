using UnityEngine;
using AlessandroBrizio.Cabinet.Core;

namespace AlessandroBrizio.Cabinet.Logging
{
    public class LogsDatabaseTable : DatabaseTable
    {
        private const string _kTableName = "Logs";
        protected override string tableName => _kTableName;
        
        private enum Column
        {
            id,
            device_name,
            game_identifier,
            game_version,
            game_buildguid,
            datetime
        }

        protected override void CreateTable()
        {
            database.ExecuteNonQuery(
                $"CREATE TABLE IF NOT EXISTS {tableName} ( " +
                    $"{Column.id} INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    $"{Column.device_name} TEXT NOT NULL, " +
                    $"{Column.game_identifier} TEXT NOT NULL, " +
                    $"{Column.game_version} TEXT NOT NULL, " +
                    $"{Column.game_buildguid} TEXT NOT NULL, " +
                    $"{Column.datetime} DATETIME DEFAULT (CURRENT_TIMESTAMP) NOT NULL" +
                $")");
        }

        public void AddLog()
        {
            AddLog(DatabaseUtility.identifier, Application.version, Application.buildGUID);
        }

        public void AddLog(string companyName, string productName, string gameVersion, string gameBuildGuid)
        {
            AddLog(DatabaseUtility.GetIdentifier(companyName, productName), gameVersion, gameBuildGuid);
        }

        public void AddLog(string gameIdentifier, string gameVersion, string gameBuildGuid)
        {
            database.ExecuteNonQuery(
                $"INSERT INTO {tableName} (" +
                    $"{Column.device_name}, " +
                    $"{Column.game_identifier}, " +
                    $"{Column.game_version}, " +
                    $"{Column.game_buildguid}" +
                $") VALUES (" +
                    $"'{SystemInfo.deviceName}', " +
                    $"'{gameIdentifier}', " +
                    $"'{gameVersion}', " +
                    $"'{gameBuildGuid}'" +
                $")");
        }

        public int GetLogCount()
        {
            return GetLogCount(DatabaseUtility.identifier);
        }

        public int GetLogCount(string companyName, string productName)
        {
            return GetLogCount(DatabaseUtility.GetIdentifier(companyName, productName));
        }

        public int GetLogCount(string gameIdentifier)
        {
            return database.ExecuteReader(
                $"SELECT COUNT(*) " +
                $"FROM {tableName} " +
                $"WHERE " +
                    $"{Column.game_identifier} = '{gameIdentifier}'",
                reader =>
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    return -1;
                });
        }
    }
}
