#***************************************
#Arguments
#%1: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Version
)

Set-StrictMode -Version Latest

$RidOperatingSystem="linux"
$OperatingSystem="ubuntu"
$OperatingSystemVersion="16"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DockerDir = Join-Path $Current docker

Set-Location -Path $DockerDir

$DockerFileDir = Join-Path $DockerDir test  | `
                 Join-Path -ChildPath $OperatingSystem | `
                 Join-Path -ChildPath $OperatingSystemVersion

# https://github.com/dotnet/coreclr/issues/9265
# linux-x86 does not support
$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; CUDA = 0;   Package = "DlibDotNet";         PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }
# $BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 32; CUDA = 0;   Package = "DlibDotNet";         PlatformTarget="x86"; Postfix = "/x86"; RID = "$RidOperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 64; CUDA = 0;   Package = "DlibDotNet.MKL";     PlatformTarget="x64"; Postfix = "/x64"; RID = "$RidOperatingSystem-x64"; }
# $BuildTargets += New-Object PSObject -Property @{Target = "mkl";  Architecture = 32; CUDA = 0;   Package = "DlibDotNet.MKL";     PlatformTarget="x86"; Postfix = "/x86"; RID = "$RidOperatingSystem-x86"; }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 92;  Package = "DlibDotNet.CUDA92";  PlatformTarget="x64"; Postfix = "";     RID = "$RidOperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 100; Package = "DlibDotNet.CUDA100"; PlatformTarget="x64"; Postfix = "";     RID = "$RidOperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 101; Package = "DlibDotNet.CUDA101"; PlatformTarget="x64"; Postfix = "";     RID = "$RidOperatingSystem-x64"; }
$BuildTargets += New-Object PSObject -Property @{Target = "cuda"; Architecture = 64; CUDA = 102; Package = "DlibDotNet.CUDA102"; PlatformTarget="x64"; Postfix = "";     RID = "$RidOperatingSystem-x64"; }

foreach($BuildTarget in $BuildTargets)
{
   $target = $BuildTarget.Target
   $cudaVersion = $BuildTarget.CUDA
   $package = $BuildTarget.Package
   $platformTarget = $BuildTarget.PlatformTarget
   $rid = $BuildTarget.RID
   $postfix = $BuildTarget.Postfix

   if ($target -ne "cuda")
   {
      $dockername = "dlibdotnet/test/$OperatingSystem/$OperatingSystemVersion/$Target" + $postfix
      $imagename  = "dlibdotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$Target" + $postfix
   }
   else
   {
      $cudaVersion = ($cudaVersion / 10).ToString("0.0")
      $dockername = "dlibdotnet/test/$OperatingSystem/$OperatingSystemVersion/$Target/$cudaVersion"
      $imagename  = "dlibdotnet/runtime/$OperatingSystem/$OperatingSystemVersion/$Target/$cudaVersion"
   }

   Write-Host "Start docker build -t $dockername $DockerFileDir --build-arg IMAGE_NAME=""$imagename""" -ForegroundColor Green
   docker build --force-rm=true -t $dockername $DockerFileDir --build-arg IMAGE_NAME="$imagename"

   if ($lastexitcode -ne 0)
   {
      Write-Host "Test Fail for $package" -ForegroundColor Red
      Set-Location -Path $Current
      exit -1
   }

   if ($BuildTarget.CUDA -ne 0)
   {
      $dockerAPIVersion = docker version --format '{{.Server.APIVersion}}'
      Write-Host "Docker API Version: $dockerAPIVersion" -ForegroundColor Yellow
      if ($dockerAPIVersion -ge 1.40)
      {
         Write-Host "Start docker run --gpus all --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
         docker run --gpus all --rm `
                     -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
                     -e "LOCAL_UID=$(id -u $env:USER)" `
                     -e "LOCAL_GID=$(id -g $env:USER)" `
                     -t "$dockername" $Version $package $platformTarget $rid
      }
      else
      {
         Write-Host "Start docker run --runtime=nvidia --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
         docker run --runtime=nvidia --rm `
                     -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
                     -e "LOCAL_UID=$(id -u $env:USER)" `
                     -e "LOCAL_GID=$(id -g $env:USER)" `
                     -t "$dockername" $Version $package $platformTarget $rid
      }
   }
   else
   {
      Write-Host "Start docker run --rm -v ""$($DlibDotNetRoot):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t ""$dockername"" $Version $package $OperatingSystem $OperatingSystemVersion" -ForegroundColor Green
      docker run --rm `
                  -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
                  -e "LOCAL_UID=$(id -u $env:USER)" `
                  -e "LOCAL_GID=$(id -g $env:USER)" `
                  -t "$dockername" $Version $package $platformTarget $rid
   }

   if ($lastexitcode -ne 0)
   {
      Set-Location -Path $Current
      exit -1
   }
}

# Move to Root directory
Set-Location -Path $Current