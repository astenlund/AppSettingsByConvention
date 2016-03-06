using System;

namespace AppSettingsByConvention
{
    /// <summary>
    /// Reads settings from the appSettings section of your configuration by convention
    /// The keys should follow the pattern CLASSNAME.PROPERTYNAME
    /// All properties on your config object need to appear in your configuration
    /// 
    /// Example usage: SettingsByConvention.ForInterface<IConfiguration>()
    /// 
    /// Inversion of Control-setup: Since the values never change after application starts,
    /// I recommend that you register as a Singleton.
    /// </summary>
    public static class SettingsByConvention
    {
        public static Func<IAppConfigValueParser> AppConfigValueParserFactory { get; set; }

        static SettingsByConvention()
        {
            AppConfigValueParserFactory = () => new AppConfigValueParser();
        }

        public static T ForClass<T>() where T : class, new()
        {
            var appConfigValueParser = AppConfigValueParserFactory();
            return new AppSettingIntoClass<T>(new AppConfigValueProvider<T>(appConfigValueParser)).Create();
        }

        public static T ForInterface<T>() where T : class
        {
            var appConfigValueParser = AppConfigValueParserFactory();
            return new AppSettingIntoInterface<T>(new AppConfigValueProvider<T>(appConfigValueParser)).Create();
        }
    }
}
