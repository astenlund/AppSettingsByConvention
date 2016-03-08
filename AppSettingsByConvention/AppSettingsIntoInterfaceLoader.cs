using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using ImpromptuInterface;

namespace AppSettingsByConvention
{
    internal interface IAppSettingIntoInterface<out TInterface> where TInterface : class
    {
        TInterface Create();
    }

    internal class AppSettingsIntoInterfaceLoader<TInterface> : IAppSettingIntoInterface<TInterface>
        where TInterface : class
    {
        private readonly PropertyInfo[] _allPropertyInfos;
        private readonly IEnumerable<IValueProvider> _appSettingValueProviders;

        public AppSettingsIntoInterfaceLoader(IEnumerable<IValueProvider> appSettingValueProviders)
        {
            var configurationType = typeof (TInterface);
            if (configurationType.IsInterface == false)
            {
                throw new InvalidOperationException($"{typeof (TInterface)} is not an interface");
            }
            _allPropertyInfos = configurationType.GetProperties();
            _appSettingValueProviders = appSettingValueProviders;
        }

        public TInterface Create()
        {
            IDictionary<string, object> instance = new ExpandoObject();
            foreach (var propertyInfo in _allPropertyInfos)
            {
                var provider = _appSettingValueProviders.FirstOrDefault(p => p.IsMatch(propertyInfo));
                if (provider == null)
                {
                    throw new InvalidOperationException($"Cannot handle properties of type {propertyInfo.PropertyType}");
                }
                var parameter = provider.GetParsedByConvention(propertyInfo);
                instance.Add(propertyInfo.Name, parameter);
            }
            return instance.ActLike<TInterface>();
        }
    }
}