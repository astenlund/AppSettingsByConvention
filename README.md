![AppSettingsByConvention](https://rawgithub.com/dignite/AppSettingsByConvention/master/icon.svg)

# AppSettingsByConvention
 Reads settings from the appSettings section of your configuration by convention.
 
 The keys should follow the pattern "CLASSNAME.PROPERTYNAME".
 
 All properties on your config object need to appear in your configuration.
 
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

## Using a class

This shows how to use this library with a class.

The target:

```C#
public class Configuration {
	public string Value { get; set; }
}
```

Your config file:

```XML
	<appSettings>
		<add key="Configuration.Value" value="ReadThis" />
	</appSettings>
```

Loading the configuration:

```C#
SettingsByConvention.ForClass<Configuration>().Create()
```

# Inversion of Control-setup
 Since the values never change after application starts, and reflection is involved in the load, I recommend that you register as a Singleton.