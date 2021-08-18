using UnityEngine;

namespace AlessandroBrizio.Cabinet.Core
{
    public static class DatabaseUtility
    {
        public static string identifier => Application.isMobilePlatform
            ? Application.identifier
            : GetIdentifier(Application.companyName, Application.productName);

        public static string GetIdentifier(string companyName, string productName)
        {
            return $"com.{companyName}.{productName}";
        }
    }
}
