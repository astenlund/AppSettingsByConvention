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
        private readonly IParser _parser;

        public AppSettingValueProvider(IParser parser)
        {
            _typeName = typeof(T).Name;
            _parser = parser;
        }

        public object GetParsedByConvention(PropertyInfo propertyInfo)
        {
            var appConfigValue = GetByConvention(propertyInfo.Name);
            return _parser.ParseIntoCorrectType(propertyInfo, appConfigValue);
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