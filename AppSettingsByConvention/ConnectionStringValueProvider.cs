using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace AppSettingsByConvention
{
    internal class ConnectionStringValueProvider<T> : IValueProvider
    {
        public bool IsMatch(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType == typeof(IConnectionString);
        }

        public object GetParsedByConvention(PropertyInfo propertyInfo)
        {
            var key = $"{typeof(T).Name}.{propertyInfo.Name}";
            var connectionStringSettings = ConfigurationManager.ConnectionStrings[key];
            if (connectionStringSettings == null)
            {
                throw new KeyNotFoundException($"ConnectionString at key {key} not found");
            }
            return new ConnectionString(connectionStringSettings);
        }
    }
}