using System;

namespace AlessandroBrizio.Cabinet.Core
{
    [Serializable]
    public struct DatabaseSettingsData : ICloneable, IEquatable<DatabaseSettingsData>
    {
        public string dataSource;

        public bool Equals(DatabaseSettingsData other)
        {
            return dataSource == other.dataSource;
        }

        public override bool Equals(object obj)
        {
            return obj is DatabaseSettingsData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return dataSource != null ? dataSource.GetHashCode() : 0;
        }

        public object Clone()
        {
            return new DatabaseSettingsData {dataSource = dataSource};
        }

        public static bool operator ==(DatabaseSettingsData left, DatabaseSettingsData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DatabaseSettingsData left, DatabaseSettingsData right)
        {
            return !(left == right);
        }
    }
}
