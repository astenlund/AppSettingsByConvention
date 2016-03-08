using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppSettingsByConvention
{
    internal interface IAppSettingIntoClass<out TPlainOldCsharpClass> where TPlainOldCsharpClass : class, new()
    {
        TPlainOldCsharpClass Create();
    }

    internal class AppSettingsIntoClassLoader<TPlainOldCsharpClass> : IAppSettingIntoClass<TPlainOldCsharpClass> where TPlainOldCsharpClass : class, new()
    {
        private readonly PropertyInfo[] _allPropertyInfos;
        private readonly IEnumerable<IValueProvider> _appSettingValueProviders;

        public AppSettingsIntoClassLoader(IEnumerable<IValueProvider> appSettingValueProviders)
        {
            _allPropertyInfos = typeof(TPlainOldCsharpClass).GetProperties();
            _appSettingValueProviders = appSettingValueProviders;
        }

        public TPlainOldCsharpClass Create()
        {
            var instance = new TPlainOldCsharpClass();
            foreach (var propertyInfo in _allPropertyInfos)
            {
                var provider = _appSettingValueProviders.First(p => p.IsMatch(propertyInfo));
                var parameter = provider.GetParsedByConvention(propertyInfo);
                propertyInfo.SetMethod.Invoke(instance, new[] { parameter });
            }
            return instance;
        }
    }
}
