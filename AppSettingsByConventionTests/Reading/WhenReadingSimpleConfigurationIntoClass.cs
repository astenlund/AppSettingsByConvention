using AppSettingsByConvention;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingSimpleConfigurationIntoClass
    {
        [Test]
        public void ShouldWork()
        {
            var expectedConfig = new SimpleConfiguration
            {
                Value1 = "Value1FromAppConfig",
                Value2 = 1337,
                Value3 = true
            };

            var config = SettingsByConvention.ForClass<SimpleConfiguration>();

            config.ShouldBeEquivalentTo(expectedConfig);
        }
    }
}
