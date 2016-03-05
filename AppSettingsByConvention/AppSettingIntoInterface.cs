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

    internal class AppSettingIntoInterface<TInterface> : IAppSettingIntoInterface<TInterface> where TInterface : class
    {
        private readonly PropertyInfo[] _allPropertyInfos;
        private readonly IAppConfigValueProvider _appConfigValueProvider;

        public AppSettingIntoInterface(IAppConfigValueProvider appConfigValueProvider)
        {
            var configurationType = typeof(TInterface);
            if (configurationType.IsInterface == false)
            {
                throw new InvalidOperationException("TInterface is not an interface");
            }
            _allPropertyInfos = configurationType.GetProperties();
            _appConfigValueProvider = appConfigValueProvider;
        }

        public TInterface Create()
        {
            IDictionary<string, object> instance = new ExpandoObject();
            foreach (var propertyInfo in _allPropertyInfos)
            {
                var parameter = _appConfigValueProvider.GetParsedByConvention(propertyInfo);
                instance.Add(propertyInfo.Name, parameter);
            }
            return instance.ActLike<TInterface>();
        }
    }
}