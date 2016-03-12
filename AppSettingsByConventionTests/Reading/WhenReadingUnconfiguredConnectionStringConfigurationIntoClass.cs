using System;
using System.Collections.Generic;
using AppSettingsByConvention;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.Reading
{
    [TestFixture]
    public class WhenReadingUnconfiguredConnectionStringConfigurationIntoClass
    {
        [Test]
        public void ShouldNotWork()
        {
            Action getConfig = () => SettingsByConvention.ForClass<UnconfiguredConnectionStringConfiguration>();
            getConfig.ShouldThrow<KeyNotFoundException>()
                .Which.Message.Should().Be("ConnectionString at key UnconfiguredConnectionStringConfiguration.ConnectionString not found");
        }

        [Test]
        public void ShouldNotWorkWithProviderName()
        {
            Action getConfig = () => SettingsByConvention.ForClass<UnconfiguredConnectionStringProviderConfiguration>();
            getConfig.ShouldThrow<KeyNotFoundException>()
                .Which.Message.Should().Be("ConnectionString at key UnconfiguredConnectionStringProviderConfiguration.ConnectionString not found");
        }
    }
}