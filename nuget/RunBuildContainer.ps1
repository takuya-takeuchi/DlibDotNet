#***************************************
#Arguments
#%1: Target (cpu/cuda/mkl/arm)
#%2: Architecture (32/64)
#%3: Platform (desktop,android/ios/uwp)
#%4: Optional Argument
#   if Target is cuda, CUDA version if Target is cuda [90/91/92/100/101/102/110/111]
#   if Target is mkl and Windows, IntelMKL directory path
#***************************************
Param
(
   [Parameter(
   Mandatory=$True,
   Position = 1
   )][string]
   $Target,

   [Parameter(
   Mandatory=$True,
   Position = 2
   )][int]
   $Architecture,

   [Parameter(
   Mandatory=$True,
   Position = 3
   )][string]
   $Platform,

   [Parameter(
   Mandatory=$True,
   Position = 4
   )][string]
   $Distribution,

   [Parameter(
   Mandatory=$True,
   Position = 5
   )][string]
   $DistributionVersion,

   [Parameter(
   Mandatory=$False,
   Position = 6
   )][string]
   $Option
)

$ScriptPath = $PSScriptRoot
$DlibDotNetRoot = Split-Path $ScriptPath -Parent
$BuildUtilsPath = Join-Path $DlibDotNetRoot "nuget" | `
                  Join-Path -ChildPath "BuildUtils.ps1"
import-module $BuildUtilsPath -function *
$Config = [Config]::new($DlibDotNetRoot, "Release", $Target, $Architecture, $Platform, $Option)

if ($Target -ne "cuda")
{
   $postfix = $Option
   $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target" + $postfix
}
else
{
   $cudaVersion = ($Option / 10).ToString("0.0")
   $dockername = "dlibdotnet/build/$Distribution/$DistributionVersion/$Target/$cudaVersion"
}

Write-Host "Start 'docker run --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t $dockername'" -ForegroundColor Green
if ($Config.HasStoreDriectory())
{
   $storeDirecotry = $Config.GetRootStoreDriectory()
   docker run --rm `
              --entrypoint="/bin/bash" `
              -v "$($storeDirecotry):/opt/data/builds" `
              -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
              -e "LOCAL_UID=$(id -u $env:USER)" `
              -e "LOCAL_GID=$(id -g $env:USER)" `
              -e "CIBuildDir=/opt/data/builds" `
              -it "$dockername"
}
else
{
   Write-Host "CIBuildDir is not set" -ForegroundColor Yellow
   docker run --rm `
              --entrypoint="/bin/bash" `
              -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
              -e "LOCAL_UID=$(id -u $env:USER)" `
              -e "LOCAL_GID=$(id -g $env:USER)" `
              -it "$dockername"
}