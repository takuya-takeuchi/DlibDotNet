#***************************************
#Arguments
#%1: Build Configuration (Release/Debug)
#%2: Target (cpu/cuda)
#%3: Architecture (32/64)
#%4: CUDA version if Target is cuda [92/100]
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Configuration,

      [Parameter(
      Mandatory=$True,
      Position = 2
      )][string]
      $Target,

      [Parameter(
      Mandatory=$True,
      Position = 3
      )][int]
      $Architecture,

      [Parameter(
      Mandatory=$False,
      ValueFromRemainingArguments=$true,
      Position = 4
      )][string[]]
      $Args
)

# Check Parameter
$ConfigurationArray = @("Debug","Release")
if($ConfigurationArray.Contains($Configuration) -eq $False){
  Write-Host "Error: Speficy Target [cpu/cuda]" -ForegroundColor Red
  exit
}

$TargetArray = @("cpu","cuda")
if($TargetArray.Contains($Target) -eq $False){
  Write-Host "Error: Speficy build configuration [Release/Debug]" -ForegroundColor Red
  exit
}

$ArchitectureArray = @(32,64)
if($ArchitectureArray.Contains($Architecture) -eq $False){
  Write-Host "Error: Speficy Architecture [32/64]" -ForegroundColor Red
  exit
}

if ($Args.Length -eq 1)
{
  $CudaVersion = [int]$Args[0]
  $CudaVersionArray = @(92,100)
  $CudaVersionHash = @{92 = "9.2"; 100 = "10.0"}
  if($CudaVersionArray.Contains($CudaVersion) -ne $True){
    Write-Host "Error: Speficy CUDA version [92/100]" -ForegroundColor Red
    exit
  }
}
else
{
  if ($Target -eq "cuda")
  {
    Write-Host "Error: Target is cuda but CUDA Version is not specified" -ForegroundColor Red
    exit
  }
}

# Set Output directory
if ($Target -eq "cpu")
{
  $OUTPUT = "build_win_cpu_x" + $Architecture
}
else
{
  $OUTPUT = "build_win_cuda-" + $CudaVersion + "_x" + $Architecture
}

# Store current directory
$Current = Get-Location

# Move to Output directory
if ((Test-Path $OUTPUT) -eq $False)
{
  New-Item $OUTPUT -ItemType Directory
}
Set-Location -Path $OUTPUT

# Invoke CMake
$VisualStudio = @{32 = "Visual Studio 15 2017"; 64 = "Visual Studio 15 2017 Win64"}
if ($Target -eq "cuda")
{
  $env:CUDA_PATH="C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v" + $CudaVersionHash[$CudaVersion]
  Write-Host $env:CUDA_PATH -ForegroundColor Green
  cmake -G $VisualStudio[$Architecture] -T host=x64 `
        -D DLIB_USE_CUDA=ON `
        ..
}
else
{
  cmake -G $VisualStudio[$Architecture] -T host=x64 `
        -D DLIB_USE_CUDA=OFF `
        ..
}

cmake --build . --config $Configuration

# Move to Root directory
Set-Location -Path $Current