using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace AppSettingsByConvention
{
    internal interface IAppConfigValueProvider
    {
        object GetParsedByConvention(PropertyInfo propertyInfo);
    }

    internal class AppConfigValueProvider<T> : IAppConfigValueProvider
    {
        private readonly string _typeName;
        private readonly IAppConfigValueParser _appConfigValueParser;

        public AppConfigValueProvider(IAppConfigValueParser appConfigValueParser)
        {
            _typeName = typeof(T).Name;
            _appConfigValueParser = appConfigValueParser;
        }

        public object GetParsedByConvention(PropertyInfo propertyInfo)
        {
            var appConfigValue = GetByConvention(propertyInfo.Name);
            return _appConfigValueParser.ParseIntoCorrectType(propertyInfo, appConfigValue);
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