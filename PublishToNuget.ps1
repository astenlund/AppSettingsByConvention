nuget pack ./AppSettingsByConvention/AppSettingsByConvention.csproj -IncludeReferencedProjects
[xml]$Nuspec = Get-Content -Path ./AppSettingsByConvention/AppSettingsByConvention.nuspec
$PackageId = $Nuspec.package.metadata.id
$PackageVersion = $Nuspec.package.metadata.version
$PackageFilename = "$PackageId.$PackageVersion.nupkg"
Write-Host "Pushing package $$PackageFilename"
nuget push "./$PackageFilename"