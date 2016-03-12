function PackAndPush {
	param(
		[Parameter(Mandatory=$true)][string]$CsprojFilePath,
		[Parameter(Mandatory=$true)][string]$NuspecFilePath
	)
	nuget pack $CsprojFilePath -IncludeReferencedProjects -Build -Symbols -Properties Configuration=Release
	
	[xml]$Nuspec = Get-Content -Path $NuspecFilePath
	$PackageId = $Nuspec.package.metadata.id
	$PackageVersion = $Nuspec.package.metadata.version
	
	$PackageFilename = "$PackageId.$PackageVersion.nupkg"
	Write-Host "Pushing package`t`t$PackageFilename"
	nuget push "./$PackageFilename"
		
	$SymbolsPackageFilename = "$PackageId.$PackageVersion.symbols.nupkg"
	Write-Host "Pushing symbols package`t$SymbolsPackageFilename"
	nuget push "./$SymbolsPackageFilename"
		
	git tag -a $PackageVersion -m "Packaged and pushed $PackageVersion to nuget.org"	
}

PackAndPush ./AppSettingsByConvention/AppSettingsByConvention.csproj ./AppSettingsByConvention/AppSettingsByConvention.nuspec