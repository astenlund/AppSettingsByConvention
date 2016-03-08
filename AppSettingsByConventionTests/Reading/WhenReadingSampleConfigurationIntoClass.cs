using System;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingSampleConfigurationIntoClass
    {
        [Test]
        public void ShouldWork()
        {
            var expectedConfig = GetExpectedConfig();

            var config = SettingsByConvention.ForClass<SampleConfiguration>();

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        [Test]
        public void ShouldWorkWithRuntimeType()
        {
            var expectedConfig = GetExpectedConfig();

            var config = SettingsByConvention.For(typeof(SampleConfiguration));

            config.ShouldBeEquivalentTo(expectedConfig, options => options.RespectingRuntimeTypes());
        }

        private static SampleConfiguration GetExpectedConfig()
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
                ConnectionString = expectedConnectionString,
                ConnectionStringWithoutProviderName = expectedConnectionString2
            };
            return expectedConfig;
        }

        [Test]
        public void ShouldNotWorkWithRuntimeTypeLackingEmptyConstructor()
        {
            const string expectedExceptionMessage = "Type" +
                                                    " AppSettingsByConventionTests.ConfigurationTargets.ClassWithoutEmptyConstructor" +
                                                    " is neither an interface nor a class with an empty constructor.";

            Action getConfig = () => SettingsByConvention.For(typeof(ClassWithoutEmptyConstructor));

            getConfig.ShouldThrow<InvalidOperationException>()
                .Which.Message.Should().Be(expectedExceptionMessage);
        }
    }
}
