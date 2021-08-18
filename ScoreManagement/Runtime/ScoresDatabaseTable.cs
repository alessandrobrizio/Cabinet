using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AlessandroBrizio.Cabinet.Core;

namespace AlessandroBrizio.Cabinet.ScoreManagement
{
    public class ScoresDatabaseTable : DatabaseTable
    {
        private const string _kTableName = "Scores";
        protected override string tableName => _kTableName;
        
        private enum Column
        {
            id,
            device_name,
            game_identifier,
            game_version,
            game_buildguid,
            name,
            score,
            seconds,
            datetime
        }

        /// <summary>
        /// Used for querying scores. Adding or removing scores always uses Application.identifier.
        /// </summary>
        public string gameIdentifier { get; set; } = DatabaseUtility.identifier;

        protected override void CreateTable()
        {
            database.ExecuteNonQuery(
                $"CREATE TABLE IF NOT EXISTS {tableName} ( " +
                    $"{Column.id} INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    $"{Column.device_name} TEXT NOT NULL, " +
                    $"{Column.game_identifier} TEXT NOT NULL, " +
                    $"{Column.game_version} TEXT NOT NULL, " +
                    $"{Column.game_buildguid} TEXT NOT NULL, " +
                    $"{Column.name} TEXT NOT NULL, " +
                    $"{Column.score} INTEGER NOT NULL, " +
                    $"{Column.seconds} INTEGER NOT NULL, " +
                    $"{Column.datetime} DATETIME DEFAULT (CURRENT_TIMESTAMP) NOT NULL" +
                $")");
        }

        public void AddScore(string name, int score, int seconds)
        {
            database.ExecuteNonQuery(
                $"INSERT INTO {tableName} (" +
                    $"{Column.device_name}, " +
                    $"{Column.game_identifier}, " +
                    $"{Column.game_version}, " +
                    $"{Column.game_buildguid}, " +
                    $"{Column.name}, " +
                    $"{Column.score}, " +
                    $"{Column.seconds}" +
                $") VALUES (" +
                    $"'{SystemInfo.deviceName}', " +
                    $"'{DatabaseUtility.identifier}', " +
                    $"'{Application.version}', " +
                    $"'{Application.buildGUID}', " +
                    $"'{name}', " +
                    $"{score}, " +
                    $"{seconds}" +
                $")");
        }

        public void RemoveAllScores()
        {
            database.ExecuteNonQuery(
                $"DELETE FROM {tableName} " +
                $"WHERE " +
                    $"{Column.game_identifier} = '{DatabaseUtility.identifier}'");
        }

        public ScoreEntry GetTopScore()
        {
            return GetTopScores(1).FirstOrDefault();
        }

        public List<ScoreEntry> GetTopScores(int amount)
        {
            return database.ExecuteReader(
                $"SELECT " +
                    $"{Column.name}, " +
                    $"{Column.score}, " +
                    $"{Column.seconds}, " +
                    $"{Column.datetime} " +
                $"FROM {tableName} " +
                $"WHERE " +
                    $"{Column.game_identifier} = '{gameIdentifier}' " +
                $"ORDER BY " +
                    $"{Column.score} DESC " +
                $"LIMIT {amount}",
                reader =>
                {
                    List<ScoreEntry> scoreEntries = new List<ScoreEntry>();
                    while (reader.Read())
                    {
                        scoreEntries.Add(new ScoreEntry
                        {
                            name = reader.GetString(0),
                            score = reader.GetInt32(1),
                            seconds = reader.GetInt32(2),
                            datetime = reader.GetDateTime(3)
                        });
                    }
                    
                    return scoreEntries;
                });
        }
    }
}
