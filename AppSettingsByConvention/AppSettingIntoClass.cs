using System.Reflection;

namespace AppSettingsByConvention
{
    internal interface IAppSettingIntoClass<out TPlainOldCsharpClass> where TPlainOldCsharpClass : class, new()
    {
        TPlainOldCsharpClass Create();
    }

    internal class AppSettingIntoClass<TPlainOldCsharpClass> : IAppSettingIntoClass<TPlainOldCsharpClass> where TPlainOldCsharpClass : class, new()
    {
        private readonly PropertyInfo[] _allPropertyInfos;
        private readonly IAppConfigValueProvider _appConfigValueProvider;

        public AppSettingIntoClass(IAppConfigValueProvider appConfigValueProvider)
        {
            _allPropertyInfos = typeof(TPlainOldCsharpClass).GetProperties();
            _appConfigValueProvider = appConfigValueProvider;
        }

        public TPlainOldCsharpClass Create()
        {
            var instance = new TPlainOldCsharpClass();
            foreach (var propertyInfo in _allPropertyInfos)
            {
                var parameter = _appConfigValueProvider.GetParsedByConvention(propertyInfo);
                propertyInfo.SetMethod.Invoke(instance, new[] { parameter });
            }
            return instance;
        }
    }
}
