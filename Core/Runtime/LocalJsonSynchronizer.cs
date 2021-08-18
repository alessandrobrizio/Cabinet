using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace AlessandroBrizio.Cabinet.Core
{
    /// <summary>
    /// Uses <see cref="OnlineString"/> to synchronize data objects that are saved to disk with <see cref="JsonUtility"/> 
    /// </summary>
    /// <typeparam name="T">Json data type</typeparam>
    public class LocalJsonSynchronizer<T> where T : ICloneable, IEquatable<T>, new()
    {
        public string path;

        private readonly T _defaultData;
        private readonly Dictionary<FieldInfo, OnlineString> _onlineStrings;

        public LocalJsonSynchronizer(string path, IDictionary<string, string> fieldUrlMapping, bool checkOnlineOnce) :
            this(path, fieldUrlMapping, checkOnlineOnce, new T())
        {
        }

        public LocalJsonSynchronizer(string path, IDictionary<string, string> fieldUrlMapping, T defaultData) :
            this(path, fieldUrlMapping, true, defaultData)
        {
        }

        public LocalJsonSynchronizer(string path, IDictionary<string, string> fieldUrlMapping) :
            this(path, fieldUrlMapping, true, new T())
        {
        }

        public LocalJsonSynchronizer(string path, IDictionary<string, string> fieldUrlMapping, bool checkOnlineOnce,
            T defaultData)
        {
            this.path = path;
            _defaultData = defaultData;
            _onlineStrings = new Dictionary<FieldInfo, OnlineString>();
            var unusedFields = new Dictionary<string, string>(fieldUrlMapping);
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields.Where(f => fieldUrlMapping.ContainsKey(f.Name)))
            {
                _onlineStrings.Add(field, new OnlineString(fieldUrlMapping[field.Name],
                    field.GetValue(_defaultData) as string,
                    checkOnlineOnce));
                unusedFields.Remove(field.Name);
            }

            foreach (var unusedField in unusedFields)
            {
                Debug.LogWarning($"Field {unusedField.Key} not found in type {typeof(T)}");
            }
        }

        public async Task<T> Read()
        {
            T data;
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                data = JsonUtility.FromJson<T>(content);
            }
            else
            {
                data = (T) _defaultData.Clone();
                Write(data);
            }

            if (_onlineStrings.All(o => o.Value.hasCheckedOnline)) return data;

            // Need to box value because structs won't work with FieldInfo.SetValue
            object onlineData = _defaultData.Clone();
            foreach (var onlineString in _onlineStrings)
            {
                string currentValue = onlineString.Key.GetValue(data) as string;
                onlineString.Value.defaultValue = string.IsNullOrEmpty(currentValue)
                    ? onlineString.Key.GetValue(_defaultData) as string
                    : currentValue;
                onlineString.Key.SetValue(onlineData, await onlineString.Value.Get());
            }

            if (!onlineData.Equals(data))
            {
                data = (T) onlineData;
                Write(data);
            }

            return data;
        }

        private void Write(T data)
        {
            string content = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, content);
        }
    }
}
