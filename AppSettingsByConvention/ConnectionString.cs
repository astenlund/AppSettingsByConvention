using System.Configuration;

namespace AppSettingsByConvention
{
    internal class ConnectionString : IConnectionString
    {
        public ConnectionString(ConnectionStringSettings connectionStringSettings)
        {
            Value = connectionStringSettings.ConnectionString;
            var providerName = connectionStringSettings.ProviderName;
            if (string.IsNullOrWhiteSpace(providerName) == false)
            {
                ProviderName = providerName;
            }
        }

        public string Value { get; }
        public string ProviderName { get; }
    }
}