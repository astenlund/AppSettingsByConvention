![AppSettingsByConvention](https://rawgithub.com/dignite/AppSettingsByConvention/master/icon.svg)

# AppSettingsByConvention
 Reads settings from the appSettings section of your configuration by convention.
 
 The keys should follow the pattern "CLASSNAME.PROPERTYNAME".
 
 All properties on your config object need to appear in your configuration.
 
 
To read a connection string value, use string property ending with ConnectionString
To read a connection string provider name, use string property ending with ConnectionStringProvider
In both cases the connection string key will be CLASSNAME.PROPERTYNAME, minus "Provider" in the case when getting the provider name.
See example below.
	
 [![Build Status](https://travis-ci.org/dignite/AppSettingsByConvention.svg?branch=master)](https://travis-ci.org/dignite/AppSettingsByConvention)
 

# Installation
 [Package @ nuget.org](https://www.nuget.org/packages/AppSettingsByConvention/)
```Powershell
    nuget install AppSettingsByConvention
```
 
# Example code

## Using an interface

This shows how to use this library with an interface.
The use for having a plain old C# class with properties is similar.

The target:

```C#
public interface IConfiguration {
	public string Value { get; }
	public string CoolConnectionString { get; }
	public string CoolConnectionStringProvider { get; }
}
```

Your config file:

```XML
    <connectionStrings>
	    <add name="IConfiguration.CoolConnectionString" connectionString="Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;" providerName="System.Data.SqlClient" />
	</connectionStrings>
	<appSettings>
		<add key="IConfiguration.Value" value="ReadThis" />
	</appSettings>
```

Loading the configuration:

```C#
SettingsByConvention.ForInterface<IConfiguration>()

// Result:
// {
//     Value = "ReadThis",
//     CoolConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;",
//     CoolConnectionStringProvider = "System.Data.SqlClient"
// }
```

## Using a class

This shows how to use this library with a class.

The target:

```C#
public class Configuration {
	public string Value { get; set; }
	public string CoolerConnectionString { get; }
	public string CoolerConnectionStringProvider { get; }
}
```

Your config file:

```XML
    <connectionStrings>
	    <add name="Configuration.CoolerConnectionString" connectionString="Server=myServerAddress;Database=myDataBase;User Id=myUsername;
Password=myPassword;" />
	</connectionStrings>
	<appSettings>
		<add key="Configuration.Value" value="ReadThis" />
	</appSettings>
```

Loading the configuration:

```C#
SettingsByConvention.ForClass<Configuration>()

// Result:
// {
//     Value = "ReadThis",
//     CoolerConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;",
//     CoolerConnectionStringProvider = null
// }
```

# Inversion of Control-setup
 Since the values never change after application starts, and reflection is involved in the load, I recommend that you register as a Singleton.