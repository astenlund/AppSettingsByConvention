using System.Reflection;

namespace AppSettingsByConvention
{
    internal class ConnectionStringProviderNameProvider<T> : ConnectionStringProviderBase<T>, IValueProvider
    {
        public bool IsMatch(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name.EndsWith("ConnectionStringProvider");
        }

        public object GetParsedByConvention(PropertyInfo propertyInfo)
        {
            var providerName = GetValue(propertyInfo).ProviderName;
            return string.IsNullOrWhiteSpace(providerName) ? null : providerName;
        }
    }
}