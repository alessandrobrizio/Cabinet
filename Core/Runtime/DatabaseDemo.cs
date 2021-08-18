using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlessandroBrizio.Cabinet.Core.Samples
{
    public abstract class DatabaseDemo<TTable> : MonoBehaviour where TTable : DatabaseTable, new()
    {
        [SerializeField] private bool _debugMode;
        [SerializeField] private string _dataSourceValueUri;
        [SerializeField] private bool _overrideDataSource;
        [SerializeField] private string _dataSource;

        private Database _database;
        protected TTable table { get; private set; }

        private async void Start()
        {
            await CreateDatabase();
        }

        private void OnDestroy()
        {
            table = null;
            _database.Dispose();
        }

        private async Task<string> GetDataSource()
        {
            if (_overrideDataSource)
            {
                return _dataSource;
            }

            DatabaseSettings.Init(_dataSourceValueUri);
            return (await DatabaseSettings.Read()).dataSource;
        }

        private async Task CreateDatabase()
        {
            CreateDatabaseFrom(await GetDataSource());
        }

        private void CreateDatabaseFrom(string dataSource)
        {
            _database = new Database(dataSource)
            {
                debugMode = _debugMode
            };
            table = _database.Get<TTable>();
        }

        public async void ReadSettings()
        {
            string dataSource = await GetDataSource();
            Debug.Log($"Data Source located at: '{dataSource}'");
            CreateDatabaseFrom(dataSource);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
