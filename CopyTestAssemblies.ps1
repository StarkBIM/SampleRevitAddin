param($SolutionDir, $TargetDir, $ConfigurationName)

if ($ConfigurationName -Match "Release")
{
	exit
}

$jsonPath = Join-Path -Path $SolutionDir -ChildPath "BuildConfig.json"

if (!(Test-Path $jsonPath))
{
	exit
}

$config = Get-Content -Raw -Path $jsonPath | ConvertFrom-Json

$baseDir = $config.InstallDirectory
$baseDir = $baseDir -replace "/", "\"
$baseDir = $baseDir -replace "%APPDATA%", $env:APPDATA
$baseDir = $baseDir -replace "%PROGRAMDATA%", $env:ProgramData
$baseDir = $baseDir -replace "%LOCALAPPDATA%", $env:LOCALAPPDATA

$versionInstallDir = Join-Path -Path $baseDir -ChildPath $ConfigurationName

echo $versionInstallDir

$testAssemblyDir = Join-Path -Path $versionInstallDir -ChildPath "TestAssemblies"

echo $testAssemblyDir

if (!(Test-Path $testAssemblyDir))
{
    New-Item -ItemType Directory -Path $testAssemblyDir -Force
}

#Copy-Item $TargetDir -Destination $testAssemblyDir -Force

$exclude = '\.tmp'

Get-ChildItem $TargetDir -Recurse  | where {$_.FullName -notmatch $exclude} | 
    Copy-Item -Destination {Join-Path $testAssemblyDir $_.FullName.Substring($TargetDir.length)} -Force