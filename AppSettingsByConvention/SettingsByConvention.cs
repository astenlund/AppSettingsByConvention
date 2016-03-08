using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace AppSettingsByConvention
{
    /// <summary>
    /// Reads settings from the appSettings section of your configuration by convention
    /// The keys should follow the pattern CLASSNAME.PROPERTYNAME
    /// All properties on your config object need to appear in your configuration
    /// 
    /// To read a connection string, use AppSettingsByConvention.IConnectionString as a property,
    /// the same naming rules apply.
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
            MethodInfo createMethod;
            if (type.IsInterface)
            {
                createMethod = typeof (SettingsByConvention).GetMethod("ForInterface").MakeGenericMethod(type);
            }
            else if (type.IsClass && type.GetConstructor(Type.EmptyTypes) != null)
            {
                createMethod = typeof(SettingsByConvention).GetMethod("ForClass").MakeGenericMethod(type);
            }
            else
            {
                throw new InvalidOperationException($"Type {type} is neither an interface nor a class with an empty constructor.");
            }
            //null, null = static method that is parameterless
            return createMethod.Invoke(null, null);
        }

        public static T ForClass<T>() where T : class, new()
        {
            return new AppSettingsIntoClassLoader<T>(GetValueProviders<T>()).Create();
        }

        public static T ForInterface<T>() where T : class
        {
            return new AppSettingsIntoInterfaceLoader<T>(GetValueProviders<T>()).Create();
        }

        private static IEnumerable<IValueProvider> GetValueProviders<T>() where T : class
        {
            var appSettingValueParser = ParserFactory();
            yield return new AppSettingValueProvider<T>(appSettingValueParser);
            yield return new ConnectionStringValueProvider<T>();
        }
    }
}
