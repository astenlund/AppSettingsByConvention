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
        private readonly IAppSettingValueProvider _appSettingValueProvider;

        public AppSettingsIntoClassLoader(IAppSettingValueProvider appSettingValueProvider)
        {
            _allPropertyInfos = typeof(TPlainOldCsharpClass).GetProperties();
            _appSettingValueProvider = appSettingValueProvider;
        }

        public TPlainOldCsharpClass Create()
        {
            var instance = new TPlainOldCsharpClass();
            foreach (var propertyInfo in _allPropertyInfos)
            {
                var parameter = _appSettingValueProvider.GetParsedByConvention(propertyInfo);
                propertyInfo.SetMethod.Invoke(instance, new[] { parameter });
            }
            return instance;
        }
    }
}
