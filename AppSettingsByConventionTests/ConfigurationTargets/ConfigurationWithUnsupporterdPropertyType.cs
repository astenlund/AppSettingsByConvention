using System.Diagnostics.CodeAnalysis;

namespace AppSettingsByConventionTests.ConfigurationTargets
{
    [ExcludeFromCodeCoverage] //Class that exists for test, but is never instansiated
    public class ConfigurationWithUnsupporterdPropertyType : IConfigurationWithUnsupporterdPropertyType
    {
        public object UnsupportedProperty { get; set; }
    }
}