using System;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingSampleConfigurationIntoInterface
    {
        [Test]
        public void ShouldWork()
        {
            var expectedConfig = GetExpectedConfig();

            var config = SettingsByConvention.ForInterface<ISampleConfiguration>();

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        [Test]
        public void ShouldWorkWithRuntimeType()
        {
            var expectedConfig = GetExpectedConfig();

            var config = SettingsByConvention.For(typeof(ISampleConfiguration));

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        private static SampleConfiguration GetExpectedConfig()
        {
            var expectedConnectionString =
                Mock.Of<IConnectionString>(x => x.Value == "CStringForInterface" && x.ProviderName == "PNameForInterface");
            var expectedConnectionString2 =
                Mock.Of<IConnectionString>(x => x.Value == "CStringForInterface2");

            var expectedConfig = new SampleConfiguration
            {
                Value1 = "InterfaceValue1FromAppConfig",
                Value2 = 1338,
                Value3 = false,
                ConnectionString = expectedConnectionString,
                ConnectionStringWithoutProviderName = expectedConnectionString2
            };
            return expectedConfig;
        }

        [Test]
        public void ShouldNotWorkWithClass()
        {
            Action getConfig = () => SettingsByConvention.ForInterface<SampleConfiguration>();

            getConfig.ShouldThrow<InvalidOperationException>();
        }
    }
}