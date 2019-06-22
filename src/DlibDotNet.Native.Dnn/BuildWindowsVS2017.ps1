#***************************************
#Arguments
#%1: Build Configuration (Release/Debug)
#%2: Target (cpu/cuda/mkl)
#%3: Architecture (32/64)
#%4: CUDA version if Target is cuda [90/91/92/100/101]
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
  Write-Host "Error: Specify build configuration [Release/Debug]" -ForegroundColor Red
  exit
}

$TargetArray = @("cpu","cuda","mkl")
if($TargetArray.Contains($Target) -eq $False){
  Write-Host "Error: Specify Target [cpu/cuda/mkl]" -ForegroundColor Red
  exit
}

$ArchitectureArray = @(32,64)
if($ArchitectureArray.Contains($Architecture) -eq $False){
  Write-Host "Error: Specify Architecture [32/64]" -ForegroundColor Red
  exit
}

# Check Args
switch ($Target) {
    "mkl"
    {
      if ($Args.Length -ne 1)
      {
        Write-Host "Error: Specify Intel MKL directory" -ForegroundColor Red
        exit
      }

      $INTELMKL_DIR = [String]$Args[0]
      if ((Test-Path $INTELMKL_DIR) -eq $False)
      {
        Write-Host "Error: Specified IntelMKL directory '${INTELMKL_DIR}' does not found" -ForegroundColor Red
        exit
      }

      $MKL_INCLUDE_DIR       = Join-Path $INTELMKL_DIR "mkl/include"
      $LIBIOMP5MD_LIB        = Join-Path $INTELMKL_DIR "compiler/lib/intel64_win/libiomp5md.lib"
      $MKLCOREDLL_LIB        = Join-Path $INTELMKL_DIR "mkl/lib/intel64_win/mkl_core_dll.lib"
      $MKLINTELLP64DLL_LIB   = Join-Path $INTELMKL_DIR "mkl/lib/intel64_win/mkl_intel_lp64_dll.lib"
      $MKLINTELTHREADDLL_LIB = Join-Path $INTELMKL_DIR "mkl/lib/intel64_win/mkl_intel_thread_dll.lib"

      if ((Test-Path $LIBIOMP5MD_LIB) -eq $False)
      {
        Write-Host "Error: ${LIBIOMP5MD_LIB} does not found" -ForegroundColor Red
        exit
      }
      if ((Test-Path $MKLCOREDLL_LIB) -eq $False)
      {
        Write-Host "Error: ${MKLCOREDLL_LIB} does not found" -ForegroundColor Red
        exit
      }
      if ((Test-Path $MKLINTELLP64DLL_LIB) -eq $False)
      {
        Write-Host "Error: ${MKLINTELLP64DLL_LIB} does not found" -ForegroundColor Red
        exit
      }
      if ((Test-Path $MKLINTELTHREADDLL_LIB) -eq $False)
      {
        Write-Host "Error: ${MKLINTELTHREADDLL_LIB} does not found" -ForegroundColor Red
        exit
      }
    }
    "cuda"
    {
      if ($Args.Length -ne 1)
      {
        Write-Host "Error: Specify CUDA version [90/91/92/100/101]" -ForegroundColor Red
        exit
      }

      $CudaVersion = [int]$Args[0]
      $CudaVersionArray = @(90,91,92,100,101)
      $CudaVersionHash = @{90 = "9.0"; 91 = "9.1"; 92 = "9.2"; 100 = "10.0"; 101 = "10.1"}
      if($CudaVersionArray.Contains($CudaVersion) -ne $True){
        Write-Host "Error: Specify CUDA version [90/91/92/100/101]" -ForegroundColor Red
        exit
      }
    }
}

# Set Output directory
switch ($Target) {
    "cpu"
    {
      $OUTPUT = "build_win_cpu_x" + $Architecture
    }
    "mkl"
    {
      $OUTPUT = "build_win_mkl_x" + $Architecture
    }
    "cuda"
    {
      $OUTPUT = "build_win_cuda-" + $CudaVersion + "_x" + $Architecture
    }
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
switch ($Target) {
    "cpu"
    {
      cmake -G $VisualStudio[$Architecture] -T host=x64 `
            -D DLIB_USE_CUDA=OFF `
            ..
    }
    "mkl"
    {
      cmake -G $VisualStudio[$Architecture] -T host=x64 `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_BLAS=ON `
            -D mkl_include_dir="${MKL_INCLUDE_DIR}" `
            -D BLAS_libiomp5md_LIBRARY="${LIBIOMP5MD_LIB}" `
            -D BLAS_mkl_core_dll_LIBRARY="${MKLCOREDLL_LIB}" `
            -D BLAS_mkl_intel_lp64_dll_LIBRARY="${MKLINTELLP64DLL_LIB}" `
            -D BLAS_mkl_intel_thread_dll_LIBRARY="${MKLINTELTHREADDLL_LIB}" `
            ..
    }
    "cuda"
    {
      $env:CUDA_PATH="C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v" + $CudaVersionHash[$CudaVersion]
      $env:PATH="$env:CUDA_PATH\bin;$env:CUDA_PATH\libnvvp;$ENV:PATH"
      Write-Host $env:CUDA_PATH -ForegroundColor Green
      cmake -G $VisualStudio[$Architecture] -T host=x64 `
            -D DLIB_USE_CUDA=ON `
            ..
    }
}

cmake --build . --config $Configuration

# Move to Root directory
Set-Location -Path $Current