class Config
{

   $ConfigurationArray =
   @(
      "Debug",
      "Release"
   )

   $TargetArray =
   @(
      "cpu",
      "cuda",
      "mkl",
      "arm",
      "android",
      "ios"
   )

   $ArchitectureArray =
   @(
      32,
      64
   )

   $CudaVersionArray =
   @(
      90,
      91,
      92,
      100,
      101
   )

   $CudaVersionHash =
   @{
      90 = "9.0";
      91 = "9.1";
      92 = "9.2";
      100 = "10.0";
      101 = "10.1"
   }

   $VisualStudioHash =
   @{
      32 = "Visual Studio 15 2017";
      64 = "Visual Studio 15 2017 Win64"
   }
   
   static $BuildLibraryWindowsHash = 
   @{
      "DlibDotNet.Native"     = "DlibDotNetNative.dll";
      "DlibDotNet.Native.Dnn" = "DlibDotNetNativeDnn.dll"
   }
   
   static $BuildLibraryLinuxHash = 
   @{
      "DlibDotNet.Native"     = "libDlibDotNetNative.so";
      "DlibDotNet.Native.Dnn" = "libDlibDotNetNativeDnn.so"
   }
   
   static $BuildLibraryOSXHash = 
   @{
      "DlibDotNet.Native"     = "libDlibDotNetNative.dylib";
      "DlibDotNet.Native.Dnn" = "libDlibDotNetNativeDnn.dylib"
   }
   
   static $BuildLibraryIOSHash = 
   @{
      "DlibDotNet.Native"     = "libDlibDotNetNative.a";
      "DlibDotNet.Native.Dnn" = "libDlibDotNetNativeDnn.a"
   }

   [string]   $_Configuration
   [int]      $_Architecture
   [string]   $_Target
   [string[]] $_MklDirectory
   [int]      $_CudaVersion
   [string]   $_AndroidABI
   [string]   $_AndroidNativeAPILevel

   #***************************************
   # Arguments
   #  %1: Build Configuration (Release/Debug)
   #  %2: Target (cpu/cuda/mkl/arm)
   #  %3: Architecture (32/64)
   #  %4: Optional Argument
   #    if Target is cuda, CUDA version if Target is cuda [90/91/92/100/101]
   #    if Target is mkl and Windows, IntelMKL directory path
   #***************************************
   Config([string]$Configuration, [string]$Target, [int]$Architecture, [string]$Option)
   {
      if ($this.ConfigurationArray.Contains($Configuration) -eq $False)
      {
         $candidate = $this.ConfigurationArray -join "/"
         Write-Host "Error: Specify build configuration [${candidate}]" -ForegroundColor Red
         exit -1
      }

      if ($this.TargetArray.Contains($Target) -eq $False)
      {
         $candidate = $this.TargetArray -join "/"
         Write-Host "Error: Specify Target [${candidate}]" -ForegroundColor Red
         exit -1
      }

      if ($this.ArchitectureArray.Contains($Architecture) -eq $False)
      {
         $candidate = $this.ArchitectureArray -join "/"
         Write-Host "Error: Specify Architecture [${candidate}]" -ForegroundColor Red
         exit -1
      }

      switch ($Target)
      {
         "cuda"
         {
            $this._CudaVersion = [int]$Option
            if ($this.CudaVersionArray.Contains($this._CudaVersion) -ne $True)
            {
               $candidate = $this.CudaVersionArray -join "/"
               Write-Host "Error: Specify CUDA version [${candidate}]" -ForegroundColor Red
               exit -1
            }
         }
         "mkl"
         {
            $this._MklDirectory = $Option
         }
         "android"
         {
            $decoded = [Config]::Base64Decode($Option)
            $setting = ConvertFrom-Json $decoded
            $this._AndroidABI            = $setting.ANDROID_ABI
            $this._AndroidNativeAPILevel = $setting.ANDROID_NATIVE_API_LEVEL
         }
      }

      $this._Configuration = $Configuration
      $this._Architecture = $Architecture
      $this._Target = $Target
   }

   static [string] Base64Encode([string]$text)
   {
      $byte = ([System.Text.Encoding]::Default).GetBytes($text)
      return [Convert]::ToBase64String($byte)
   }

   static [string] Base64Decode([string]$base64)
   {
      $byte = [System.Convert]::FromBase64String($base64)
      return [System.Text.Encoding]::Default.GetString($byte)
   }

   static [hashtable] GetBinaryLibraryWindowsHash()
   {
      return [Config]::BuildLibraryWindowsHash
   }

   static [hashtable] GetBinaryLibraryOSXHash()
   {
      return [Config]::BuildLibraryOSXHash
   }

   static [hashtable] GetBinaryLibraryLinuxHash()
   {
      return [Config]::BuildLibraryLinuxHash
   }

   static [hashtable] GetBinaryLibraryIOSHash()
   {
      return [Config]::BuildLibraryIOSHash
   }

   [int] GetArchitecture()
   {
      return $this._Architecture
   }

   [string] GetConfigurationName()
   {
      return $this._Configuration
   }

   [string] GetAndroidABI()
   {
      return $this._AndroidABI
   }

   [string] GetAndroidNativeAPILevel()
   {
      return $this._AndroidNativeAPILevel
   }

   [string] GetArtifactDirectoryName()
   {
      $target = $this._Target

      if ($this._Target -eq "cuda")
      {
         $cudaVersion = $this._CudaVersion
         return "${target}-${cudaVersion}"
      }
      else
      {
         return $target
      }
   }

   [string] GetOSName()
   {
      $os = ""

      if ($global:IsWindows)
      {
         $os = "win"
      }
      elseif ($global:IsMacOS)
      {
         $os = "osx"
      }
      elseif ($global:IsLinux)
      {
         $os = "linux"
      }
      else
      {
         Write-Host "Error: This plaform is not support" -ForegroundColor Red
         exit -1
      }

      return $os
   }

   [string] GetIntelMklDirectory()
   {
      return [string]$this._MklDirectory
   }

   [string] GetArchitectureName()
   {
      $arch = ""

      if ($this._Architecture -eq 32)
      {
         $arch = "x86"
      }
      elseif ($this._Architecture -eq 64)
      {
         $arch = "x64"
      }

      return $arch
   }

   [string] GetBuildDirectoryName([string]$os="")
   {
      if ($os)
      {
         $osname = $os
      }
      else
      {
         $osname = $this.GetOSName()
      }
      
      $target = $this._Target
      $architecture = $this.GetArchitectureName()

      if ($target -eq "cuda")
      {
         $version = $this._CudaVersion
         return "build_${osname}_cuda-${version}_${architecture}"
      }
      elseif ($target -eq "android")
      {
         $abi = $this._AndroidABI
         return "build_${osname}_${target}-${abi}"
      }
      else
      {
         return "build_${osname}_${target}_${architecture}"
      }
   }

   [string] GetVisualStudio()
   {
      return $this.VisualStudioHash[$this._Architecture]
   }

   [string] GetCUDAPath()
   {
      return "C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v" + $this.CudaVersionHash[$this._CudaVersion]
   }

}

function ConfigCPU([Config]$Config)
{
   if ($IsWindows)
   {
      cmake -G $Config.GetVisualStudio() -T host=x64 `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_LAPACK=OFF `
            ..
   }
   else
   {
      cmake -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D mkl_include_dir="" `
            -D mkl_intel="" `
            -D mkl_rt="" `
            -D mkl_thread="" `
            -D mkl_pthread="" `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            ..
   }
}

function ConfigCUDA([Config]$Config)
{
   if ($IsWindows)
   {
      $cudaPath = $Config.GetCUDAPath()
      $env:CUDA_PATH="${cudaPath}"
      $env:PATH="$env:CUDA_PATH\bin;$env:CUDA_PATH\libnvvp;$ENV:PATH"
      Write-Host $env:CUDA_PATH -ForegroundColor Green

      cmake -G $Config.GetVisualStudio() -T host=x64 `
            -D DLIB_USE_CUDA=ON `
            -D DLIB_USE_BLAS=OFF `
            -D DLIB_USE_LAPACK=OFF `
            ..
   }
   else
   {
      cmake -D DLIB_USE_CUDA=ON `
            -D DLIB_USE_BLAS=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D LIBPNG_IS_GOOD=OFF  `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            ..
   }
}

function ConfigMKL([Config]$Config)
{
   if ($IsWindows)
   {
      $intelMklDirectory = $Config.GetIntelMklDirectory()
      if (!$intelMklDirectory) {
         Write-Host "Error: Specify Intel MKL directory" -ForegroundColor Red
         exit -1
      }

      if ((Test-Path $intelMklDirectory) -eq $False) {
         Write-Host "Error: Specified IntelMKL directory '${intelMklDirectory}' does not found" -ForegroundColor Red
         exit -1
      }

      $MKL_INCLUDE_DIR = Join-Path $intelMklDirectory "mkl/include"
      $LIBIOMP5MD_LIB = Join-Path $intelMklDirectory "compiler/lib/intel64_win/libiomp5md.lib"
      $MKLCOREDLL_LIB = Join-Path $intelMklDirectory "mkl/lib/intel64_win/mkl_core_dll.lib"
      $MKLINTELLP64DLL_LIB = Join-Path $intelMklDirectory "mkl/lib/intel64_win/mkl_intel_lp64_dll.lib"
      $MKLINTELTHREADDLL_LIB = Join-Path $intelMklDirectory "mkl/lib/intel64_win/mkl_intel_thread_dll.lib"

      if ((Test-Path $LIBIOMP5MD_LIB) -eq $False) {
         Write-Host "Error: ${LIBIOMP5MD_LIB} does not found" -ForegroundColor Red
         exit -1
      }
      if ((Test-Path $MKLCOREDLL_LIB) -eq $False) {
         Write-Host "Error: ${MKLCOREDLL_LIB} does not found" -ForegroundColor Red
         exit -1
      }
      if ((Test-Path $MKLINTELLP64DLL_LIB) -eq $False) {
         Write-Host "Error: ${MKLINTELLP64DLL_LIB} does not found" -ForegroundColor Red
         exit -1
      }
      if ((Test-Path $MKLINTELTHREADDLL_LIB) -eq $False) {
         Write-Host "Error: ${MKLINTELTHREADDLL_LIB} does not found" -ForegroundColor Red
         exit -1
      }

      cmake -G $Config.GetVisualStudio() -T host=x64 `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_BLAS=ON `
            -D DLIB_USE_LAPACK=OFF `
            -D mkl_include_dir="${MKL_INCLUDE_DIR}" `
            -D BLAS_libiomp5md_LIBRARY="${LIBIOMP5MD_LIB}" `
            -D BLAS_mkl_core_dll_LIBRARY="${MKLCOREDLL_LIB}" `
            -D BLAS_mkl_intel_lp64_dll_LIBRARY="${MKLINTELLP64DLL_LIB}" `
            -D BLAS_mkl_intel_thread_dll_LIBRARY="${MKLINTELTHREADDLL_LIB}" `
            ..
   }
   else
   {
      cmake -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_BLAS=ON `
            -D DLIB_USE_LAPACK=OFF `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            ..
   }
}

function ConfigARM([Config]$Config)
{
   if ($Config.GetArchitecture() -eq 32)
   {
      cmake -D DLIB_USE_CUDA=OFF `
            -D ENABLE_NEON=ON `
            -D DLIB_USE_BLAS=ON `
            -D DLIB_USE_LAPACK=OFF `
            -D CMAKE_C_COMPILER="/usr/bin/arm-linux-gnueabihf-gcc" `
            -D CMAKE_CXX_COMPILER="/usr/bin/arm-linux-gnueabihf-g++" `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            ..
   }
   else
   {
      cmake -D DLIB_USE_CUDA=OFF `
            -D ENABLE_NEON=ON `
            -D DLIB_USE_BLAS=ON `
            -D DLIB_USE_LAPACK=OFF `
            -D CMAKE_C_COMPILER="/usr/bin/aarch64-linux-gnu-gcc" `
            -D CMAKE_CXX_COMPILER="/usr/bin/aarch64-linux-gnu-g++" `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            ..
   }
}

function ConfigANDROID([Config]$Config)
{
   if ($IsLinux)
   {
      if (!${env:ANDROID_NDK_HOME})
      {
         Write-Host "Error: Specify ANDROID_NDK_HOME environmental value" -ForegroundColor Red
         exit -1
      }

      if ((Test-Path "${env:ANDROID_NDK_HOME}/build/cmake/android.toolchain.cmake") -eq $False)
      {
         Write-Host "Error: Specified Android NDK toolchain '${env:ANDROID_NDK_HOME}/build/cmake/android.toolchain.cmake' does not found" -ForegroundColor Red
         exit -1
      }

      $level = $Config.GetAndroidNativeAPILevel()
      $abi = $Config.GetAndroidABI()

      cmake -G Ninja `
            -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK_HOME}/build/cmake/android.toolchain.cmake `
            -D ANDROID_NDK=${env:ANDROID_NDK_HOME} `
            -D CMAKE_MAKE_PROGRAM=ninja `
            -D ANDROID_NATIVE_API_LEVEL=${level} `
            -D ANDROID_ABI=${abi} `
            -D ANDROID_TOOLCHAIN=clang `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_BLAS=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D mkl_include_dir="" `
            -D mkl_intel="" `
            -D mkl_rt="" `
            -D mkl_thread="" `
            -D mkl_pthread="" `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            -D DLIB_NO_GUI_SUPPORT=ON `
            ..
   }
   else
   {      
      Write-Host "Error: This platform can not build android binary" -ForegroundColor Red
      exit -1
   }
}

function ConfigIOS([Config]$Config)
{
   if ($IsMacOS)
   {
      cmake -G Xcode `
            -D CMAKE_TOOLCHAIN_FILE=../../ios-cmake/ios.toolchain.cmake `
            -D PLATFORM=OS64COMBINED `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_BLAS=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D mkl_include_dir="" `
            -D mkl_intel="" `
            -D mkl_rt="" `
            -D mkl_thread="" `
            -D mkl_pthread="" `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            -D DLIB_NO_GUI_SUPPORT=ON `
            ..
   }
   else
   {      
      Write-Host "Error: This platform can not build iOS binary" -ForegroundColor Red
      exit -1
   }
}

function Build([Config]$Config)
{
   $Current = Get-Location

   $Output = $Config.GetBuildDirectoryName("")
   if ((Test-Path $Output) -eq $False)
   {
      New-Item $Output -ItemType Directory
   }

   Set-Location -Path $Output

   switch ($Target)
   {
      "cpu"
      {
         ConfigCPU $Config
      }
      "mkl"
      {
         ConfigMKL $Config
      }
      "cuda"
      {
         ConfigCUDA $Config
      }
      "arm"
      {
         ConfigARM $Config
      }
      "android"
      {
         ConfigANDROID $Config
      }
      "ios"
      {
         ConfigIOS $Config
      }
   }

   cmake --build . --config $Config.GetConfigurationName()

   # Move to Root directory
   Set-Location -Path $Current
}

function CopyToArtifact()
{
   Param([string]$srcDir, [string]$build, [string]$libraryName, [string]$dstDir, [string]$rid, [string]$configuration="")

   if ($configuration)
   {
      $binary = Join-Path ${srcDir} ${build}  | `
               Join-Path -ChildPath ${configuration} | `
               Join-Path -ChildPath ${libraryName}
   }
   else
   {
      $binary = Join-Path ${srcDir} ${build}  | `
               Join-Path -ChildPath ${libraryName}
   }

   $output = Join-Path $dstDir runtimes | `
            Join-Path -ChildPath ${rid} | `
            Join-Path -ChildPath native | `
            Join-Path -ChildPath $libraryName

   Write-Host "Copy ${libraryName} to ${output}" -ForegroundColor Green
   Copy-Item ${binary} ${output}
}