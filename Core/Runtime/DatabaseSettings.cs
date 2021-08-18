using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace AlessandroBrizio.Cabinet.Core
{
    public static class DatabaseSettings
    {
        public const string kDefaultFileName = "database.settings";

        private static LocalJsonSynchronizer<DatabaseSettingsData> _databaseSettingsDataSynchronizer;

        public static string path
        {
            get => _databaseSettingsDataSynchronizer.path;
            set => _databaseSettingsDataSynchronizer.path = value;
        }

        public static void Init(string dataSourceValueUri)
        {
            if (string.IsNullOrEmpty(dataSourceValueUri))
            {
                throw new System.ArgumentNullException(nameof(dataSourceValueUri));
            }

            _databaseSettingsDataSynchronizer = new LocalJsonSynchronizer<DatabaseSettingsData>(
                $"{Application.persistentDataPath}/{kDefaultFileName}",
                new Dictionary<string, string> {{nameof(DatabaseSettingsData.dataSource), dataSourceValueUri}}
            );
        }

        public static async Task<DatabaseSettingsData> Read()
        {
            if (_databaseSettingsDataSynchronizer == null)
            {
                throw new System.InvalidOperationException("You must call Init() method first.");
            }

            return await _databaseSettingsDataSynchronizer.Read();
        }

        public static void Write(DatabaseSettingsData data)
        {
            string content = JsonUtility.ToJson(data);
            File.WriteAllText(path, content);
        }
    }
}
