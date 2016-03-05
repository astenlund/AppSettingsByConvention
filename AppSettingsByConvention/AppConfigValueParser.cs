using System;
using System.Reflection;

namespace AppSettingsByConvention
{
    public interface IAppConfigValueParser
    {
        object ParseIntoCorrectType(PropertyInfo propertyInfo, string appConfigValue);
    }

    internal class AppConfigValueParser : IAppConfigValueParser
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