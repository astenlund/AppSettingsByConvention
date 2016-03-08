using AppSettingsByConvention;
using FluentAssertions;
using NUnit.Framework;

namespace AppSettingsByConventionTests
{
    [TestFixture]
    public class WhenAnalyzingExportedTypes
    {
        [Test]
        public void ShouldMatchWhitelist()
        {
            var whiteList = new []
            {
                typeof(SettingsByConvention),
                typeof(IConnectionString),
                typeof(IParser)
            };

            var exportedTypes = typeof (SettingsByConvention).Assembly.GetExportedTypes();

            exportedTypes.ShouldBeEquivalentTo(whiteList);
        }
    }
}
