using System;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingConfigurationWithUnsupporterdPropertyTypeIntoClass
    {
        [Test]
        public void ShouldNotWork()
        {
            Action getConfig = () => SettingsByConvention.ForClass<ConfigurationWithUnsupporterdPropertyType>();
            getConfig.ShouldThrow<UnsupportedPropertyTypeException>()
                .Which.Message.Should().Be("Cannot handle properties of type System.Object! You can support it if you add to the Dictionary SettingsByConvention.ParserMappings.");
        }
    }
}