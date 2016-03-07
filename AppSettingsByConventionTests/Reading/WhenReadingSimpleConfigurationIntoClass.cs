using System;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
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

        [Test]
        public void ShouldWorkWithRuntimeType()
        {
            var expectedConfig = new SimpleConfiguration
            {
                Value1 = "Value1FromAppConfig",
                Value2 = 1337,
                Value3 = true
            };

            var config = SettingsByConvention.For(typeof(SimpleConfiguration));

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
