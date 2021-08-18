using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace AlessandroBrizio.Cabinet.Core
{
    public class OnlineString
    {
        public static TimeSpan timeout
        {
            get => _timeout;
            set
            {
                _timeout = value;
                if (_httpClient != null)
                {
                    _httpClient.Timeout = _timeout;
                }
            }
        }

        private static uint _instancesCount;
        private static HttpClient _httpClient;
        private static TimeSpan _timeout = TimeSpan.FromSeconds(5f);

        private string _cachedValue = string.Empty;

        public string uri { get; }
        public string defaultValue { get; set; }
        public bool checkOnlineOnce { get; }
        public bool hasCheckedOnline { get; private set; }

        private static void AddInstance()
        {
            if (_instancesCount == 0)
            {
                _httpClient = new HttpClient {Timeout = timeout};
            }

            _instancesCount++;
        }

        private static void RemoveInstance()
        {
            _instancesCount--;
            if (_instancesCount == 0)
            {
                _httpClient = null;
            }
        }

        public OnlineString(string url, string defaultValue) : this(url, defaultValue, checkOnlineOnce: true)
        {
        }

        public OnlineString(string url, string defaultValue, bool checkOnlineOnce)
        {
            uri = url;
            this.defaultValue = defaultValue;
            this.checkOnlineOnce = checkOnlineOnce;
            AddInstance();
        }

        ~OnlineString()
        {
            RemoveInstance();
        }

        public async Task<string> Get()
        {
            if (hasCheckedOnline && checkOnlineOnce) return _cachedValue;
            hasCheckedOnline = true;
            try
            {
                _cachedValue = await _httpClient.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                _cachedValue = defaultValue;
            }

            return _cachedValue;
        }
    }
}
