using AppSettingsByConvention;

namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public class UnconfiguredConnectionStringConfiguration
    {
        public IConnectionString NotInAppConfig { get; set; }
    }
}