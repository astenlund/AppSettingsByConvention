namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public class ConfigurationWithUnsupporterdPropertyType : IConfigurationWithUnsupporterdPropertyType
    {
        public object UnsupportedProperty { get; set; }
    }
}