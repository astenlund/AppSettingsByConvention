namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public interface IConfigurationWithUnsupporterdPropertyType
    {
        object UnsupportedProperty { get; }
    }
}