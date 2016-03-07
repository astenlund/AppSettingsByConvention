using System;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
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

        [Test]
        public void ShouldWorkWithRuntimeType()
        {
            var expectedConfig = new SimpleConfiguration
            {
                Value1 = "InterfaceValue1FromAppConfig",
                Value2 = 1338,
                Value3 = false
            };

            var config = SettingsByConvention.For(typeof(ISimpleConfiguration));

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        [Test]
        public void ShouldNotWorkWithClass()
        {
            Action getConfig = () => SettingsByConvention.ForInterface<SimpleConfiguration>();

            getConfig.ShouldThrow<InvalidOperationException>();
        }
    }
}