nuget pack ./AppSettingsByConvention/AppSettingsByConvention.csproj -IncludeReferencedProjects -Build -Symbols -Properties Configuration=Release

[xml]$Nuspec = Get-Content -Path ./AppSettingsByConvention/AppSettingsByConvention.nuspec
$PackageId = $Nuspec.package.metadata.id
$PackageVersion = $Nuspec.package.metadata.version

$PackageFilename = "$PackageId.$PackageVersion.nupkg"
Write-Host "Pushing package`t`t$PackageFilename"
nuget push "./$PackageFilename"
	
$SymbolsPackageFilename = "$PackageId.$PackageVersion.symbols.nupkg"
Write-Host "Pushing symbols package`t$SymbolsPackageFilename"
nuget push "./$SymbolsPackageFilename"
	
git tag -a $PackageVersion -m "Packaged and pushed $PackageVersion to nuget.org"