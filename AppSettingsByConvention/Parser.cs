using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppSettingsByConvention
{
    public interface IParser
    {
        object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue);
    }

    internal class Parser : IParser
    {
        private readonly Dictionary<Type, Func<string, object>> _parsers;

        public Parser(Dictionary<Type, Func<string, object>> parsers)
        {
            _parsers = parsers;
        }

        public object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue)
        {
            var propertyType = propertyInfo.PropertyType;
            if (_parsers.ContainsKey(propertyType) == false)
            {
                throw new InvalidOperationException($"Cannot handle properties of type {propertyType}");
            }
            return _parsers[propertyType].Invoke(appConfigValue);
        }
    }
}