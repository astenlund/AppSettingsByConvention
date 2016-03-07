using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using ImpromptuInterface;

namespace AppSettingsByConvention
{
    public interface IAppSettingIntoInterface<out TInterface> where TInterface : class
    {
        TInterface Create();
    }

    internal class AppSettingsIntoInterfaceLoader<TInterface> : IAppSettingIntoInterface<TInterface> where TInterface : class
    {
        private readonly PropertyInfo[] _allPropertyInfos;
        private readonly IAppSettingValueProvider _appSettingValueProvider;

        public AppSettingsIntoInterfaceLoader(IAppSettingValueProvider appSettingValueProvider)
        {
            var configurationType = typeof(TInterface);
            if (configurationType.IsInterface == false)
            {
                throw new InvalidOperationException($"{typeof (TInterface)} is not an interface");
            }
            _allPropertyInfos = configurationType.GetProperties();
            _appSettingValueProvider = appSettingValueProvider;
        }

        public TInterface Create()
        {
            IDictionary<string, object> instance = new ExpandoObject();
            foreach (var propertyInfo in _allPropertyInfos)
            {
                var parameter = _appSettingValueProvider.GetParsedByConvention(propertyInfo);
                instance.Add(propertyInfo.Name, parameter);
            }
            return instance.ActLike<TInterface>();
        }
    }
}