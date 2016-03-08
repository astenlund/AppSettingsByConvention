namespace AppSettingsByConvention
{
    public interface IConnectionString
    {
        string Value { get; }
        string ProviderName { get; }
    }
}