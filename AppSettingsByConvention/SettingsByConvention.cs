using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppSettingsByConvention
{
    /// <summary>
    /// Reads settings from the appSettings section of your configuration by convention
    /// The keys should follow the pattern CLASSNAME.PROPERTYNAME
    /// All properties on your config object need to appear in your configuration
    /// 
    /// Example use:
    ///   SettingsByConvention.ForInterface<IConfiguration>()
    ///   SettingsByConvention.ForClass<Configuration>() 
    ///   SettingsByConvention.For(typeof(IConfigiration)) 
    ///   SettingsByConvention.For(typeof(Configiration))
    ///  
    /// Inversion of Control-setup: Since the values never change after application starts,
    /// I recommend that you register as a Singleton.
    /// </summary>
    public static class SettingsByConvention
    {
        public static readonly Dictionary<Type, Func<string, object>> ParserMappings;
        public static Func<IParser> ParserFactory { get; set; }

        static SettingsByConvention()
        {
            ParserMappings = new Dictionary<Type, Func<string, object>>
                {
                    {typeof(string), input => input},
                    {typeof(int), input => int.Parse(input)},
                    {typeof(bool), input => bool.Parse(input)}
                };
            ParserFactory = () => new Parser(ParserMappings);
        }

        public static object For(Type type)
        {
            var parser = ParserFactory();
            var closedProviderType = typeof (AppSettingValueProvider<>).MakeGenericType(type);
            var appConfigValueProvider = Activator.CreateInstance(closedProviderType, parser);

            Type appSettingLoaderType;
            if (type.IsInterface)
            {
                appSettingLoaderType = typeof (AppSettingsIntoInterfaceLoader<>).MakeGenericType(type);
            }
            else if (type.IsClass && type.GetConstructor(Type.EmptyTypes) != null)
            {
                appSettingLoaderType = typeof(AppSettingsIntoClassLoader<>).MakeGenericType(type);
            }
            else
            {
                throw new InvalidOperationException($"Type {type} is neither an interface nor a class with an empty constructor.");
            }
            var appSettingLoader = Activator.CreateInstance(appSettingLoaderType, appConfigValueProvider);
            return appSettingLoaderType.GetMethod("Create").Invoke(appSettingLoader, new object[0]);
        }

        public static T ForClass<T>() where T : class, new()
        {
            return new AppSettingsIntoClassLoader<T>(GetAppSettingValueProvider<T>()).Create();
        }

        public static T ForInterface<T>() where T : class
        {
            return new AppSettingsIntoInterfaceLoader<T>(GetAppSettingValueProvider<T>()).Create();
        }

        private static IAppSettingValueProvider GetAppSettingValueProvider<T>() where T : class
        {
            var appConfigValueParser = ParserFactory();
            return new AppSettingValueProvider<T>(appConfigValueParser);
        }
    }
}
