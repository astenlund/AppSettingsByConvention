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
        public IConnectionString ConnectionString { get; set; }
        public IConnectionString ConnectionStringWithoutProviderName { get; set; }


        internal static SampleConfiguration GetExpectedConfig()
        {
            var expectedConnectionString =
                Mock.Of<IConnectionString>(x => x.Value == "CString" && x.ProviderName == "PName");
            var expectedConnectionString2 =
                Mock.Of<IConnectionString>(x => x.Value == "CString2");

            var expectedConfig = new SampleConfiguration
            {
                Value1 = "Value1FromAppConfig",
                Value2 = 1337,
                Value3 = true,
                List = new List<string>
                {
                    "one", "two", "three"
                },
                Array = new[] { "1", "2", "3" },
                ConnectionString = expectedConnectionString,
                ConnectionStringWithoutProviderName = expectedConnectionString2
            };
            return expectedConfig;
        }
    }
}