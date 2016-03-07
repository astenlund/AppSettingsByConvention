using System;
using System.Collections.Generic;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingUnconfiguredConfigurationIntoInterface
    {
        [Test]
        public void ShouldNotWork()
        {
            Action getConfig = () => SettingsByConvention.ForInterface<IUnconfiguredConfiguration>();
            getConfig.ShouldThrow<KeyNotFoundException>()
                .Which.Message.Should().Be("Value at key IUnconfiguredConfiguration.NotInAppConfig not found");
        }
    }
}