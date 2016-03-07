using System;
using System.Reflection;

namespace AppSettingsByConvention
{
    public interface IValueParser
    {
        object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue);
    }

    internal class ValueParser : IValueParser
    {
        public object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue)
        {
            object parameter;
            var propertyType = propertyInfo.PropertyType;
            if (propertyType == typeof (string))
            {
                parameter = appConfigValue;
            }
            else if (propertyType == typeof (int))
            {
                parameter = int.Parse(appConfigValue);
            }
            else if (propertyType == typeof(bool))
            {
                parameter = bool.Parse(appConfigValue);
            }
            else
            {
                throw new InvalidOperationException($"Cannot handle properties of type {propertyType}");
            }
            return parameter;
        }
    }
}