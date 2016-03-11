using System;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingSampleConfigurationIntoClass
    {
        [Test]
        public void ShouldWork()
        {
            var expectedConfig = SampleConfiguration.GetExpectedConfig();

            var config = SettingsByConvention.ForClass<SampleConfiguration>();

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        [Test]
        public void ShouldWorkWithRuntimeType()
        {
            var expectedConfig = SampleConfiguration.GetExpectedConfig();

            var config = SettingsByConvention.For(typeof(SampleConfiguration));

            config.ShouldBeEquivalentTo(expectedConfig, options => options.RespectingRuntimeTypes());
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
