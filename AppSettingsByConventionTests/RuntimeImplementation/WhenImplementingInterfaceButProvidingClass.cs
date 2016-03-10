using System;
using AppSettingsByConvention.RuntimeInterfaceImplementation;
using AppSettingsByConventionTests.ConfigurationTargets;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests.RuntimeImplementation
{
    [TestFixture]
    public class WhenImplementingInterfaceButProvidingClass
    {
        [Test]
        public void ShouldGetException()
        {
            Action implementClassWithProperties = () => typeof (SampleConfiguration).ImplementClassWithProperties();

            implementClassWithProperties.ShouldThrow<InvalidOperationException>();
        }
    }
}
