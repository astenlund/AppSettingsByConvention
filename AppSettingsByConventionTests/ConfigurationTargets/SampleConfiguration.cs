using System.Collections.Generic;
using AppSettingsByConvention;
using Moq;

namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public class SampleConfiguration : ISampleConfiguration
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Value3 { get; set; }
        public List<string> List { get; set; }
        public string[] Array { get; set; }
        public string ConnectionString { get; set; }
        public string ConnectionStringProvider { get; set; }
        public string ImplicitProviderConnectionString { get; set; }
        public string ImplicitProviderConnectionStringProvider { get; set; }


        internal static SampleConfiguration GetExpectedConfig()
        {
            return new SampleConfiguration
            {
                Value1 = "Value1FromAppConfig",
                Value2 = 1337,
                Value3 = true,
                List = new List<string>
                {
                    "one", "two", "three"
                },
                Array = new[] { "1", "2", "3" },
                ConnectionString = "CString",
                ConnectionStringProvider = "PName",
                ImplicitProviderConnectionString = "CString2",
                ImplicitProviderConnectionStringProvider = null
            };
        }
    }
}