namespace AppSettingsByConventionTests.ConfigurationTargets
{
    public interface ISimpleConfiguration
    {
        string Value1 { get; }
        int Value2 { get; }
        bool Value3 { get; }
    }
}