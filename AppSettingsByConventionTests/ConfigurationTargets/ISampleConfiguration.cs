using System.Collections.Generic;

namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public interface ISampleConfiguration
    {
        string Value1 { get; }
        int Value2 { get; }
        bool Value3 { get; }
        List<string> List { get; }
        string[] Array { get; }
        string ConnectionString { get; }
        string ConnectionStringProvider { get; }
        string ImplicitProviderConnectionString { get; }
        string ImplicitProviderConnectionStringProvider { get; }
    }
}