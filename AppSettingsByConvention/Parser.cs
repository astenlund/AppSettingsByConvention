using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppSettingsByConvention
{
    public interface IParser
    {
        bool IsMatch(PropertyInfo propertyInfo);
        object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue);
    }

    internal class Parser : IParser
    {
        private readonly Dictionary<Type, Func<string, object>> _parsers;

        public Parser(Dictionary<Type, Func<string, object>> parsers)
        {
            _parsers = parsers;
        }

        public bool IsMatch(PropertyInfo propertyInfo)
        {
            return _parsers.ContainsKey(propertyInfo.PropertyType);
        }

        public object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue)
        {
            return _parsers[propertyInfo.PropertyType].Invoke(appConfigValue);
        }
    }
}