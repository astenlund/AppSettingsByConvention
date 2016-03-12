using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace AppSettingsByConvention
{
    internal class ConnectionStringProviderBase<T>
    {
        private const string SuffixToRemove = "Provider";

        protected static ConnectionStringSettings GetValue(PropertyInfo propertyInfo)
        {
            var propertyName = GetNormalizedPropertyName(propertyInfo);
            var key = $"{typeof(T).Name}.{propertyName}";
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[key];
            if (connectionStringSettings == null)
            {
                throw new KeyNotFoundException($"ConnectionString at key {key} not found");
            }
            return connectionStringSettings;
        }

        private static string GetNormalizedPropertyName(PropertyInfo propertyInfo)
        {
            var untouchedPropertyName = propertyInfo.Name;
            if (untouchedPropertyName.EndsWith(SuffixToRemove))
            {
                return untouchedPropertyName.Substring(0, untouchedPropertyName.Length - SuffixToRemove.Length);
            }
            return untouchedPropertyName;
        }
    }
}