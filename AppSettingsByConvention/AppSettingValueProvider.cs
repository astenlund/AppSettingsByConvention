using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace AppSettingsByConvention
{
    internal interface IAppSettingValueProvider
    {
        object GetParsedByConvention(PropertyInfo propertyInfo);
    }

    internal class AppSettingValueProvider<T> : IAppSettingValueProvider
    {
        private readonly string _typeName;
        private readonly IValueParser _valueParser;

        public AppSettingValueProvider(IValueParser valueParser)
        {
            _typeName = typeof(T).Name;
            _valueParser = valueParser;
        }

        public object GetParsedByConvention(PropertyInfo propertyInfo)
        {
            var appConfigValue = GetByConvention(propertyInfo.Name);
            return _valueParser.ParseIntoCorrectType(propertyInfo, appConfigValue);
        }

        private string GetByConvention(string propertyName)
        {
            var key = $"{_typeName}.{propertyName}";
            var appConfigValue = ConfigurationManager.AppSettings[key];
            if (appConfigValue == null)
            {
                throw new KeyNotFoundException($"Value at key {key} not found");
            }
            return appConfigValue;
        }
    }
}