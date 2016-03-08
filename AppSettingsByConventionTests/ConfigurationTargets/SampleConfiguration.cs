using AppSettingsByConvention;

namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public class SampleConfiguration : ISampleConfiguration
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Value3 { get; set; }
        public IConnectionString ConnectionString { get; set; }
        public IConnectionString ConnectionStringWithoutProviderName { get; set; }
    }
}