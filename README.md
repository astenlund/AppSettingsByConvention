# AppSettingsByConvention
 Reads settings from the appSettings section of your configuration by convention.
 The keys should follow the pattern "CLASSNAME.PROPERTYNAME".
 All properties on your config object need to appear in your configuration.
 
# Example usage

The target:

```C#
public interface IConfiguration {
	public string Value { get; }
}
```

Your config file:

```XML
	<appSettings>
		<add key="IConfiguration.Value" value="ReadThis" />
	</appSettings>
```

Loading the configuration:

```C#
SettingsByConvention.ForInterface<IConfiguration>().Create()
```
 
# Inversion of Control-setup
 Since the values never change after application starts, and reflection is involved in the load, I recommend that you register as a Singleton.