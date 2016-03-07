namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public class SimpleConfiguration : ISimpleConfiguration
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Value3 { get; set; }
    }
}