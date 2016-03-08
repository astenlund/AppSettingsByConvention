using AppSettingsByConvention;

namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public interface ISampleConfiguration
    {
        string Value1 { get; }
        int Value2 { get; }
        bool Value3 { get; }
        IConnectionString ConnectionString { get; }
        IConnectionString ConnectionStringWithoutProviderName { get; }
    }
}