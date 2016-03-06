using AppSettingsByConvention;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingSimpleConfigurationIntoInterface
    {
        [Test]
        public void ShouldWork()
        {
            var expectedConfig = new SimpleConfiguration
            {
                Value1 = "InterfaceValue1FromAppConfig",
                Value2 = 1338,
                Value3 = false
            };

            var config = SettingsByConvention.ForInterface<ISimpleConfiguration>();

            config.ShouldBeEquivalentTo(expectedConfig);
        }
    }
}