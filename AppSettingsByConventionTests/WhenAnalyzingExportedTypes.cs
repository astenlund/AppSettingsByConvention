using System.Linq;
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
                typeof(IParser),
                typeof(UnsupportedPropertyTypeException)
            };

            var exportedTypes = typeof (SettingsByConvention).Assembly.GetExportedTypes();

            exportedTypes.ShouldBeEquivalentTo(whiteList, "that is the whitelist, but found unexpected types "+string.Join(", ", exportedTypes.Except(whiteList)));
        }
    }
}
