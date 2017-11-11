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

#$versionInstallDir = Join-Path -Path $baseDir -ChildPath $ConfigurationName

#echo $versionInstallDir

#$addinDir = Join-Path -Path $versionInstallDir -ChildPath "Addin"

#echo $addinDir

if (!(Test-Path $baseDir))
{
    New-Item -ItemType Directory -Path $baseDir -Force
}

try
{
    Copy-Item $TargetDir -Destination $baseDir -Container -Recurse -Force -ErrorAction Stop
}
catch
{
    #If the copy fails, then it is likely because the DLL is currently in use by Revit
    #So, we want to copy the files to a new directory that will be scanned for by the GenericCommand class
    $dirName = [System.IO.Path]::GetFileName($TargetDir)

    $installDir = Join-Path -Path $baseDir -ChildPath $dirName

    $updatedAssembliesDir = Join-Path $installDir -ChildPath "UpdatedAssemblies"

    if (!(Test-Path $updatedAssembliesDir))
    {
        New-Item -ItemType Directory -Path $updatedAssembliesDir -Force
    }

    $timestamp = Get-Date -Format yyyyMMddHHmmss

    $timestampDir = Join-Path -Path $updatedAssembliesDir -ChildPath $timestamp

    Copy-Item $TargetDir -Destination $timestampDir -Container -Recurse -Force
}