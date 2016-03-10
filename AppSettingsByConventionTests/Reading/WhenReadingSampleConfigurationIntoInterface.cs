using System;
using System.Collections.Generic;
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
            var expectedConfig = SampleConfiguration.GetExpectedConfig();

            var config = SettingsByConvention.ForInterface<ISampleConfiguration>();

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        [Test]
        public void ShouldWorkWithRuntimeType()
        {
            var expectedConfig = SampleConfiguration.GetExpectedConfig();

            var config = SettingsByConvention.For(typeof(ISampleConfiguration));

            config.ShouldBeEquivalentTo(expectedConfig);
        }

        [Test]
        public void ShouldNotWorkWithClass()
        {
            Action getConfig = () => SettingsByConvention.ForInterface<SampleConfiguration>();

            getConfig.ShouldThrow<InvalidOperationException>();
        }
    }
}