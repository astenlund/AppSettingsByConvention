using System.Reflection;

namespace AppSettingsByConvention
{
    internal class ConnectionStringValueProvider<T> : ConnectionStringProviderBase<T>, IValueProvider
    {
        public bool IsMatch(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name.EndsWith("ConnectionString");
        }

        public object GetParsedByConvention(PropertyInfo propertyInfo)
        {
            return GetValue(propertyInfo).ConnectionString;
        }
    }
}