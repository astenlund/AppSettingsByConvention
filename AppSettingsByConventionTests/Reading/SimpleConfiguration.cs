namespace AppSettingsByConventionTests.Reading
{
    public interface ISimpleConfiguration
    {
        string Value1 { get; }
        int Value2 { get; }
        bool Value3 { get; }
    }

    public class SimpleConfiguration : ISimpleConfiguration
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Value3 { get; set; }
    }
}