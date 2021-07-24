class BuildTarget
{
   [string] $Platform
   [string] $Target
   [int]    $Architecture
   [string] $Postfix
   [string] $RID

   BuildTarget( [string]$Platform,
                [string]$Target,
                [int]   $Architecture,
                [string]$RID,
                [string]$Postfix = ""
              )
   {
      $this.Platform = $Platform
      $this.Target = $Target
      $this.Architecture = $Architecture
      $this.Postfix = $Postfix
      $this.RID = $RID
   }

   BuildTarget( [string]$Platform,
                [string]$Target,
                [int]   $Architecture,
                [string]$RID,
                [string]$Postfix = "",
                [int]   $CudaVersion = 0
              )
   {
      $this.Platform = $Platform
      $this.Target = $Target
      $this.Architecture = $Architecture
      $this.Postfix = $Postfix
      $this.RID = $RID
      $this.CudaVersion = $CudaVersion
   }

   BuildTarget( [string]$Platform,
                [string]$Target,
                [int]   $Architecture,
                [string]$RID,
                [string]$Postfix = "",
                [string]$MklDiretory = ""
              )
   {
      $this.Platform = $Platform
      $this.Target = $Target
      $this.Architecture = $Architecture
      $this.Postfix = $Postfix
      $this.RID = $RID
      $this.MklDiretory = $MklDiretory
   }

   [string] $OperatingSystem
   [string] $Distribution
   [string] $DistributionVersion

   [string] $CudaVersion

   [string] $AndroidVersion
   [string] $AndroidNativeApiLevel

   [string] $MklDiretory
}

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
      "arm"
   )

   $PlatformArray =
   @(
      "desktop",
      "android",
      "ios",
      "uwp"
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
      101,
      102,
      110,
      111,
      112
   )

   $CudaVersionHash =
   @{
      90 = "CUDA_PATH_V9_0";
      91 = "CUDA_PATH_V9_1";
      92 = "CUDA_PATH_V9_2";
      100 = "CUDA_PATH_V10_0";
      101 = "CUDA_PATH_V10_1";
      102 = "CUDA_PATH_V10_2";
      110 = "CUDA_PATH_V11_0";
      111 = "CUDA_PATH_V11_1";
      112 = "CUDA_PATH_V11_2";
   }

   $VisualStudio = "Visual Studio 15 2017"

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

   [string]   $_Root
   [string]   $_Configuration
   [int]      $_Architecture
   [string]   $_Target
   [string]   $_Platform
   [string]   $_MklDirectory
   [int]      $_CudaVersion
   [string]   $_AndroidABI
   [string]   $_AndroidNativeAPILevel
   [string]   $_OSXArchitectures

   #***************************************
   # Arguments
   #  %1: Root directory of DlibDotNet
   #  %2: Build Configuration (Release/Debug)
   #  %3: Target (cpu/cuda/mkl/arm)
   #  %4: Architecture (32/64)
   #  %5: Platform (desktop/android/ios/uwp)
   #  %6: Optional Argument
   #    if Target is cuda, CUDA version if Target is cuda [90/91/92/100/101/102/110/111/112]
   #    if Target is mkl and Windows, IntelMKL directory path
   #***************************************
   Config(  [string]$Root,
            [string]$Configuration,
            [string]$Target,
            [int]   $Architecture,
            [string]$Platform,
            [string]$Option
         )
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

      if ($this.PlatformArray.Contains($Platform) -eq $False)
      {
         $candidate = $this.PlatformArray -join "/"
         Write-Host "Error: Specify Platform [${candidate}]" -ForegroundColor Red
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
      }

      switch ($Platform)
      {
         "android"
         {
            $decoded = [Config]::Base64Decode($Option)
            $setting = ConvertFrom-Json $decoded
            $this._AndroidABI            = $setting.ANDROID_ABI
            $this._AndroidNativeAPILevel = $setting.ANDROID_NATIVE_API_LEVEL
         }
         "ios"
         {
            $this._OSXArchitectures = $Option
         }
      }

      $this._Root = $Root
      $this._Configuration = $Configuration
      $this._Architecture = $Architecture
      $this._Target = $Target
      $this._Platform = $Platform
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

   [string] GetRootDir()
   {
      return $this._Root
   }

   [string] GetToolchainDir()
   {
      return   Join-Path $this.GetRootDir() src |
               Join-Path -ChildPath toolchains
   }

   [string] GetDlibRootDir()
   {
      return   Join-Path $this.GetRootDir() src |
               Join-Path -ChildPath dlib
   }

   [string] GetNugetDir()
   {
      return   Join-Path $this.GetRootDir() nuget
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
      $platform = $this._Platform
      $name = ""

      switch ($platform)
      {
         "desktop"
         {
            if ($target -eq "cuda")
            {
               $cudaVersion = $this._CudaVersion
               $name = "${target}-${cudaVersion}"
            }
            else
            {
               $name = $target
            }
         }
         "android"
         {
            $name = $platform
         }
         "ios"
         {
            $name = $platform
         }
         "uwp"
         {
            $name = Join-Path $platform $target
         }
      }

      return $name
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
         if (![string]::IsNullOrEmpty($this._OSXArchitectures))
         {
            $os = "ios"
         }
         else
         {
            $os = "osx"
         }
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
      $target = $this._Target
      $architecture = $this._Architecture

      if ($target -eq "arm")
      {
         if ($architecture -eq 32)
         {
            $arch = "arm"
         }
         elseif ($architecture -eq 64)
         {
            $arch = "arm64"
         }
      }
      else
      {
         if ($architecture -eq 32)
         {
            $arch = "x86"
         }
         elseif ($architecture -eq 64)
         {
            $arch = "x64"
         }
      }

      return $arch
   }

   [string] GetTarget()
   {
      return $this._Target
   }

   [string] GetPlatform()
   {
      return $this._Platform
   }

   [string] GetRootStoreDriectory()
   {
      return $env:CIBuildDir
   }

   [string] GetStoreDriectory([string]$CMakefileDir)
   {
      $DirectoryName = Split-Path $CMakefileDir -leaf
      $buildDir = $this.GetRootStoreDriectory()
      if (!(Test-Path($buildDir)))
      {
         return $CMakefileDir
      }

      return Join-Path $buildDir "DlibDotNet" | `
             Join-Path -ChildPath $DirectoryName
   }

   [bool] HasStoreDriectory()
   {
      $buildDir = $this.GetRootStoreDriectory()
      return Test-Path($buildDir)
   }

   [string] GetBuildDirectoryName([string]$os="")
   {
      if (![string]::IsNullOrEmpty($os))
      {
         $osname = $os
      }
      elseif (![string]::IsNullOrEmpty($env:TARGETRID))
      {
         $osname = $env:TARGETRID
      }
      else
      {
         $osname = $this.GetOSName()
      }

      $target = $this._Target
      $platform = $this._Platform
      $architecture = $this.GetArchitectureName()

      switch ($platform)
      {
         "android"
         {
            $architecture = $this._AndroidABI
         }
         "ios"
         {
            $architecture = $this._OSXArchitectures
         }
      }

      $postfix = ""
      if ($this._Configuration -eq "Debug")
      {
         $postfix = "_d"
      }

      if ($target -eq "cuda")
      {
         $version = $this._CudaVersion
         return "build_${osname}_${platform}_cuda-${version}_${architecture}${postfix}"
      }
      else
      {
         return "build_${osname}_${platform}_${target}_${architecture}${postfix}"
      }
   }

   [string] GetVisualStudio()
   {
      return $this.VisualStudio
   }

   [string] GetVisualStudioArchitecture()
   {
      $architecture = $this._Architecture
      $target = $this._Target

      if ($target -eq "arm")
      {
         if ($architecture -eq 32)
         {
            return "ARM"
         }
         elseif ($architecture -eq 64)
         {
            return "ARM64"
         }
      }
      else
      {
         if ($architecture -eq 32)
         {
            return "Win32"
         }
         elseif ($architecture -eq 64)
         {
            return "x64"
         }
      }

      Write-Host "${architecture} and ${target} do not support" -ForegroundColor Red
      exit -1
   }

   [string] GetCUDAPath()
   {
      # CUDA_PATH_V9_0=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v9.0
      # CUDA_PATH_V9_1=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v9.1
      # CUDA_PATH_V9_2=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v9.2
      # CUDA_PATH_V10_0=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v10.0
      # CUDA_PATH_V10_1=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v10.1
      # CUDA_PATH_V10_2=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v10.2
      # CUDA_PATH_V11_0=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v11.0
      # CUDA_PATH_V11_1=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v11.1
      # CUDA_PATH_V11_2=C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA\v11.2
      $version = $this.CudaVersionHash[$this._CudaVersion]
      return [environment]::GetEnvironmentVariable($version, 'Machine')
   }

   [string] GetAVXINSTRUCTIONS()
   {
      return "ON"
   }

   [string] GetSSE4INSTRUCTIONS()
   {
      return "ON"
   }

   [string] GetSSE2INSTRUCTIONS()
   {
      return "OFF"
   }

   [string] GetToolchainFile()
   {
      $architecture = $this._Architecture
      $target = $this._Target
      $toolchainDir = $this.GetToolchainDir()
      $toolchain = Join-Path $toolchainDir "empty.cmake"

      if ($global:IsLinux)
      {
         if ($target -eq "arm")
         {
            if ($architecture -eq 64)
            {
               $toolchain = Join-Path $toolchainDir "aarch64-linux-gnu.toolchain.cmake"
            }
         }
      }
      else
      {
         $Platform = $this._Platform
         switch ($Platform)
         {
            "ios"
            {
               $osxArchitectures = $this.GetOSXArchitectures()
               $toolchain = Join-Path $toolchainDir "${osxArchitectures}-ios.cmake"
            }
         }
      }

      return $toolchain
   }

   [string] GetDeveloperDir()
   {
      return $env:DEVELOPER_DIR
   }

   [string] GetOSXArchitectures()
   {
      return $this._OSXArchitectures
   }

   [string] GetIOSSDK([string]$osxArchitectures, [string]$developerDir)
   {
      switch ($osxArchitectures)
      {
         "arm64e"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "arm64"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "arm"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "armv7"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "armv7s"
         {
            return "${developerDir}/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk"
         }
         "i386"
         {
            return "${developerDir}/Platforms/iPhoneSimulator.platform/Developer/SDKs/iPhoneSimulator.sdk"
         }
         "x86_64"
         {
            return "${developerDir}/Platforms/iPhoneSimulator.platform/Developer/SDKs/iPhoneSimulator.sdk"
         }
      }
      return $this._OSXArchitectures
   }

   static [bool] Build([string]$root, [bool]$docker, [hashtable]$buildHashTable, [BuildTarget]$buildTarget)
   {
      $current = $PSScriptRoot

      $platform              = $buildTarget.Platform
      $target                = $buildTarget.Target
      $architecture          = $buildTarget.Architecture
      $postfix               = $buildTarget.Postfix
      $rid                   = $buildTarget.RID
      $operatingSystem       = $buildTarget.OperatingSystem
      $distribution          = $buildTarget.Distribution
      $distributionVersion   = $buildTarget.DistributionVersion
      $cudaVersion           = $buildTarget.CudaVersion
      $androidVersion        = $buildTarget.AndroidVersion
      $androidNativeApiLevel = $buildTarget.AndroidNativeApiLevel
      $mklDiretory           = $buildTarget.MklDiretory
      $configuration         = "Release"

      $option = ""

      $sourceRoot = Join-Path $root src

      if ($docker -eq $True)
      {
         $dockerDir = Join-Path $root docker

         Set-Location -Path $dockerDir

         $dockerFileDir = Join-Path $dockerDir build  | `
                          Join-Path -ChildPath $distribution | `
                          Join-Path -ChildPath $distributionVersion

         if ($platform -eq "android")
         {
            $setting =
            @{
               'ANDROID_ABI' = $rid;
               'ANDROID_NATIVE_API_LEVEL' = $androidNativeApiLevel
            }
            $option = [Config]::Base64Encode((ConvertTo-Json -Compress $setting))

            $dockername = "dlibdotnet/build/$distribution/$distributionVersion/android/$androidVersion"
            $imagename  = "dlibdotnet/devel/$distribution/$distributionVersion/android/$androidVersion"
         }
         else
         {
            if ($target -ne "cuda")
            {
               $option = ""

               $dockername = "dlibdotnet/build/$distribution/$distributionVersion/$Target" + $postfix
               $imagename  = "dlibdotnet/devel/$distribution/$distributionVersion/$Target" + $postfix
            }
            else
            {
               $option = $cudaVersion

               $cudaVersion = ($cudaVersion / 10).ToString("0.0")
               $dockername = "dlibdotnet/build/$distribution/$distributionVersion/$Target/$cudaVersion"
               $imagename  = "dlibdotnet/devel/$distribution/$distributionVersion/$Target/$cudaVersion"
            }
         }

         $config = [Config]::new($root, $configuration, $target, $architecture, $platform, $option)
         $libraryDir = Join-Path "artifacts" $config.GetArtifactDirectoryName()
         $build = $config.GetBuildDirectoryName($operatingSystem)

         Write-Host "Start 'docker build -t $dockername $dockerFileDir --build-arg IMAGE_NAME=""$imagename""'" -ForegroundColor Green
         docker build --network host --force-rm=true -t $dockername $dockerFileDir --build-arg IMAGE_NAME="$imagename" | Write-Host

         if ($lastexitcode -ne 0)
         {
            Write-Host "Failed to docker build: $lastexitcode" -ForegroundColor Red
            return $False
         }

         if ($platform -eq "desktop")
         {
            if ($target -eq "arm")
            {
               Write-Host "Start 'docker run --rm --privileged multiarch/qemu-user-static --reset -p yes'" -ForegroundColor Green
               docker run --rm --privileged multiarch/qemu-user-static --reset -p yes
            }
         }

         # Build binary
         foreach ($key in $buildHashTable.keys)
         {
            Write-Host "Start 'docker run --rm -v ""$($root):/opt/data/DlibDotNet"" -e LOCAL_UID=$(id -u $env:USER) -e LOCAL_GID=$(id -g $env:USER) -t $dockername'" -ForegroundColor Green
            docker run --rm --network host `
                        -v "$($root):/opt/data/DlibDotNet" `
                        -e "LOCAL_UID=$(id -u $env:USER)" `
                        -e "LOCAL_GID=$(id -g $env:USER)" `
                        -t "$dockername" $key $target $architecture $platform $option | Write-Host

            if ($lastexitcode -ne 0)
            {
               Write-Host "Failed to docker run: $lastexitcode" -ForegroundColor Red
               return $False
            }
         }

         # Copy output binary
         foreach ($key in $buildHashTable.keys)
         {
            $srcDir = Join-Path $sourceRoot $key
            $dll = $buildHashTable[$key]
            $dstDir = Join-Path $current $libraryDir

            CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
         }
      }
      else
      {
         if ($platform -eq "ios")
         {
            $option = $rid
         }
         else
         {
            if ($target -eq "mkl")
            {
               $option = $mklDiretory
            }
            elseif ($target -eq "cuda")
            {
               $option = $cudaVersion
            }
         }

         $config = [Config]::new($root, $configuration, $target, $architecture, $platform, $option)
         $libraryDir = Join-Path "artifacts" $config.GetArtifactDirectoryName()
         $build = $config.GetBuildDirectoryName($operatingSystem)

         foreach ($key in $buildHashTable.keys)
         {
            $srcDir = Join-Path $sourceRoot $key

            # Move to build target directory
            Set-Location -Path $srcDir

            $arc = $config.GetArchitectureName()
            Write-Host "Build $key [$arc] for $target" -ForegroundColor Green
            Build -Config $config

            if ($lastexitcode -ne 0)
            {
               return $False
            }
         }

         # Copy output binary
         foreach ($key in $buildHashTable.keys)
         {
            $srcDir = Join-Path $sourceRoot $key
            $dll = $buildHashTable[$key]
            $dstDir = Join-Path $current $libraryDir

            if ($global:IsWindows)
            {
               CopyToArtifact -configuration "Release" -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
            }
            else
            {
               CopyToArtifact -srcDir $srcDir -build $build -libraryName $dll -dstDir $dstDir -rid $rid
            }
         }
      }

      return $True
   }

}

function ConfigCPU([Config]$Config, [string]$CMakefileDir)
{
   if ($global:IsWindows)
   {
      $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
      $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
      $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

      $vs = $Config.GetVisualStudio()
      $vsarch = $Config.GetVisualStudioArchitecture()

      Write-Host "   cmake -G `"${vs}`" -A $vsarch -T host=x64 `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -G "${vs}" -A $vsarch -T host=x64 `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
            -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
            -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
            ${CMakefileDir}
   }
   elseif ($global:IsMacOS)
   {
      # Use static libjpeg
      $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
      $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
      $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

      $arch_type = $Config.GetArchitecture()
      Write-Host "   cmake -D ARCH_TYPE=`"${arch_type}`" `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D mkl_include_dir=`"`" `
         -D mkl_intel=`"`" `
         -D mkl_rt=`"`" `
         -D mkl_thread=`"`" `
         -D mkl_pthread=`"`" `
         -D LIBPNG_IS_GOOD=OFF `
         -D PNG_FOUND=OFF `
         -D PNG_LIBRARY_RELEASE=`"`" `
         -D PNG_LIBRARY_DEBUG=`"`" `
         -D PNG_PNG_INCLUDE_DIR=`"`" `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         -D JPEG_FOUND=OFF `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -D ARCH_TYPE="${arch_type}" `
            -D DLIB_USE_CUDA=OFF `
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
            -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
            -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
            -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
            -D JPEG_FOUND=OFF `
            ${CMakefileDir}
   }
   else
   {
      $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
      $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
      $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

      $arch_type = $Config.GetArchitecture()
      Write-Host "   cmake -D ARCH_TYPE=`"$arch_type`" `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D mkl_include_dir=`"`" `
         -D mkl_intel=`"`" `
         -D mkl_rt=`"`" `
         -D mkl_thread=`"`" `
         -D mkl_pthread=`"`" `
         -D LIBPNG_IS_GOOD=OFF `
         -D PNG_FOUND=OFF `
         -D PNG_LIBRARY_RELEASE=`"`" `
         -D PNG_LIBRARY_DEBUG=`"`" `
         -D PNG_PNG_INCLUDE_DIR=`"`" `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -D ARCH_TYPE="$arch_type" `
            -D DLIB_USE_CUDA=OFF `
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
            -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
            -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
            -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
            ${CMakefileDir}
   }
}

function ConfigCUDA([Config]$Config, [string]$CMakefileDir)
{
   if ($global:IsWindows)
   {
      $cudaPath = $Config.GetCUDAPath()
      if (!(Test-Path $cudaPath))
      {
         Write-Host "Error: '${cudaPath}' does not found" -ForegroundColor Red
         exit -1
      }

      $env:CUDA_PATH="${cudaPath}"
      $env:PATH="$env:CUDA_PATH\bin;$env:CUDA_PATH\libnvvp;$ENV:PATH"
      Write-Host "Info: CUDA_PATH: ${env:CUDA_PATH}" -ForegroundColor Green

      $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
      $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
      $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

      $vs = $Config.GetVisualStudio()
      $vsarch = $Config.GetVisualStudioArchitecture()

      Write-Host "   cmake -G `"${vs}`" -A $vsarch -T host=x64 `
         -D DLIB_USE_CUDA=ON `
         -D DLIB_USE_BLAS=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         -D CUDA_NVCC_FLAGS=`"--expt-relaxed-constexpr`" `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -G "${vs}" -A $vsarch -T host=x64 `
            -D DLIB_USE_CUDA=ON `
            -D DLIB_USE_BLAS=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
            -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
            -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
            -D CUDA_NVCC_FLAGS="--expt-relaxed-constexpr" `
            ${CMakefileDir}
   }
   else
   {
      $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
      $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
      $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

      Write-Host "   cmake -D DLIB_USE_CUDA=ON `
         -D DLIB_USE_BLAS=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D LIBPNG_IS_GOOD=OFF  `
         -D PNG_FOUND=OFF `
         -D PNG_LIBRARY_RELEASE="" `
         -D PNG_LIBRARY_DEBUG=`"`" `
         -D PNG_PNG_INCLUDE_DIR=`"`" `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         -D CUDA_NVCC_FLAGS=`"--expt-relaxed-constexpr`" `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -D DLIB_USE_CUDA=ON `
            -D DLIB_USE_BLAS=OFF `
            -D DLIB_USE_LAPACK=OFF `
            -D LIBPNG_IS_GOOD=OFF  `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
            -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
            -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
            -D CUDA_NVCC_FLAGS="--expt-relaxed-constexpr" `
            ${CMakefileDir}
   }
}

function ConfigMKL([Config]$Config, [string]$CMakefileDir)
{
   if ($global:IsWindows)
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

      $architecture = $Config.GetArchitecture()
      $architectureDir = ""
      switch ($architecture)
      {
         32
         {
            $architectureDir = "ia32_win"
            $MKL_INCLUDE_DIR = Join-Path $intelMklDirectory "mkl/include"
            $LIBIOMP5MD_LIB = Join-Path $intelMklDirectory "compiler/lib/${architectureDir}/libiomp5md.lib"
            $MKLCOREDLL_LIB = Join-Path $intelMklDirectory "mkl/lib/${architectureDir}/mkl_core_dll.lib"
            $MKLINTELC_LIB = Join-Path $intelMklDirectory "mkl/lib/${architectureDir}/mkl_intel_c.lib"
            $MKLINTELTHREADDLL_LIB = Join-Path $intelMklDirectory "mkl/lib/${architectureDir}/mkl_intel_thread_dll.lib"

            if ((Test-Path $LIBIOMP5MD_LIB) -eq $False) {
               Write-Host "Error: ${LIBIOMP5MD_LIB} does not found" -ForegroundColor Red
               exit -1
            }
            if ((Test-Path $MKLCOREDLL_LIB) -eq $False) {
               Write-Host "Error: ${MKLCOREDLL_LIB} does not found" -ForegroundColor Red
               exit -1
            }
            if ((Test-Path $MKLINTELC_LIB) -eq $False) {
               Write-Host "Error: ${MKLINTELC_LIB} does not found" -ForegroundColor Red
               exit -1
            }
            if ((Test-Path $MKLINTELTHREADDLL_LIB) -eq $False) {
               Write-Host "Error: ${MKLINTELTHREADDLL_LIB} does not found" -ForegroundColor Red
               exit -1
            }

            $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
            $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
            $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

            $vs = $Config.GetVisualStudio()
            $vsarch = $Config.GetVisualStudioArchitecture()

            Write-Host "   cmake -G `"${vs}`" -A $vsarch -T host=x64 `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_BLAS=ON `
         -D DLIB_USE_LAPACK=OFF `
         -D mkl_include_dir=`"${MKL_INCLUDE_DIR}`" `
         -D BLAS_libiomp5md_LIBRARY=`"${LIBIOMP5MD_LIB}`" `
         -D BLAS_mkl_core_dll_LIBRARY=`"${MKLCOREDLL_LIB}`" `
         -D BLAS_mkl_intel_c_dll_LIBRARY=`"${MKLINTELC_LIB}`" `
         -D BLAS_mkl_intel_thread_dll_LIBRARY=`"${MKLINTELTHREADDLL_LIB}`" `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         ${CMakefileDir}" -ForegroundColor Yellow
            cmake -G "${vs}" -A $vsarch -T host=x64 `
                  -D DLIB_USE_CUDA=OFF `
                  -D DLIB_USE_BLAS=ON `
                  -D DLIB_USE_LAPACK=OFF `
                  -D mkl_include_dir="${MKL_INCLUDE_DIR}" `
                  -D BLAS_libiomp5md_LIBRARY="${LIBIOMP5MD_LIB}" `
                  -D BLAS_mkl_core_dll_LIBRARY="${MKLCOREDLL_LIB}" `
                  -D BLAS_mkl_intel_c_dll_LIBRARY="${MKLINTELC_LIB}" `
                  -D BLAS_mkl_intel_thread_dll_LIBRARY="${MKLINTELTHREADDLL_LIB}" `
                  -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
                  -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
                  -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
                  ${CMakefileDir}
         }
         64
         {
            $architectureDir = "intel64_win"
            $MKL_INCLUDE_DIR = Join-Path $intelMklDirectory "mkl/include"
            $LIBIOMP5MD_LIB = Join-Path $intelMklDirectory "compiler/lib/${architectureDir}/libiomp5md.lib"
            $MKLCOREDLL_LIB = Join-Path $intelMklDirectory "mkl/lib/${architectureDir}/mkl_core_dll.lib"
            $MKLINTELLP64DLL_LIB = Join-Path $intelMklDirectory "mkl/lib/${architectureDir}/mkl_intel_lp64_dll.lib"
            $MKLINTELTHREADDLL_LIB = Join-Path $intelMklDirectory "mkl/lib/${architectureDir}/mkl_intel_thread_dll.lib"

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

            $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
            $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
            $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

            $vs = $Config.GetVisualStudio()
            $vsarch = $Config.GetVisualStudioArchitecture()

            Write-Host "   cmake -G `"${vs}`" -A $vsarch -T host=x64 `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_BLAS=ON `
         -D DLIB_USE_LAPACK=OFF `
         -D mkl_include_dir=`"${MKL_INCLUDE_DIR}`" `
         -D BLAS_libiomp5md_LIBRARY=`"${LIBIOMP5MD_LIB}`" `
         -D BLAS_mkl_core_dll_LIBRARY=`"${MKLCOREDLL_LIB}`" `
         -D BLAS_mkl_intel_lp64_dll_LIBRARY=`"${MKLINTELLP64DLL_LIB}`" `
         -D BLAS_mkl_intel_thread_dll_LIBRARY=`"${MKLINTELTHREADDLL_LIB}`" `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         ${CMakefileDir}" -ForegroundColor Yellow
            cmake -G "${vs}" -A $vsarch -T host=x64 `
                  -D DLIB_USE_CUDA=OFF `
                  -D DLIB_USE_BLAS=ON `
                  -D DLIB_USE_LAPACK=OFF `
                  -D mkl_include_dir="${MKL_INCLUDE_DIR}" `
                  -D BLAS_libiomp5md_LIBRARY="${LIBIOMP5MD_LIB}" `
                  -D BLAS_mkl_core_dll_LIBRARY="${MKLCOREDLL_LIB}" `
                  -D BLAS_mkl_intel_lp64_dll_LIBRARY="${MKLINTELLP64DLL_LIB}" `
                  -D BLAS_mkl_intel_thread_dll_LIBRARY="${MKLINTELTHREADDLL_LIB}" `
                  -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
                  -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
                  -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
                  ${CMakefileDir}
         }
      }
   }
   else
   {
      $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
      $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
      $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

      $arch_type = $Config.GetArchitecture()
      Write-Host "   cmake -D ARCH_TYPE=`"$arch_type`" `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_BLAS=ON `
         -D DLIB_USE_LAPACK=OFF `
         -D LIBPNG_IS_GOOD=OFF `
         -D PNG_FOUND=OFF `
         -D PNG_LIBRARY_RELEASE=`"`" `
         -D PNG_LIBRARY_DEBUG=`"`" `
         -D PNG_PNG_INCLUDE_DIR=`"`" `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -D ARCH_TYPE="$arch_type" `
            -D DLIB_USE_CUDA=OFF `
            -D DLIB_USE_BLAS=ON `
            -D DLIB_USE_LAPACK=OFF `
            -D LIBPNG_IS_GOOD=OFF `
            -D PNG_FOUND=OFF `
            -D PNG_LIBRARY_RELEASE="" `
            -D PNG_LIBRARY_DEBUG="" `
            -D PNG_PNG_INCLUDE_DIR="" `
            -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
            -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
            -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
            ${CMakefileDir}
   }
}

function ConfigARM([Config]$Config, [string]$CMakefileDir)
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
            ${CMakefileDir}
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
            ${CMakefileDir}
   }
}

function ConfigUWP([Config]$Config, [string]$CMakefileDir)
{
   if ($global:IsWindows)
   {
      # apply patch
      $patch = "uwp.patch"
      $nugetDir = $Config.GetNugetDir()
      $dlibDir = $Config.GetDlibRootDir()
      $patchFullPath = Join-Path $nugetDir $patch
      $current = Get-Location
      Set-Location -Path $dlibDir
      Write-Host "Apply ${patch} to ${dlibDir}" -ForegroundColor Yellow
      Write-Host "git apply ""${patchFullPath}""" -ForegroundColor Yellow
      git apply """${patchFullPath}"""
      Set-Location -Path $current

      $vs = $Config.GetVisualStudio()
      $vsarch = $Config.GetVisualStudioArchitecture()

      if ($Config.GetTarget() -eq "arm")
      {
         Write-Host "   cmake -G `"${vs}`" -A $vsarch -T host=x64 `
         -D CMAKE_SYSTEM_NAME=WindowsStore `
         -D USE_AVX_INSTRUCTIONS:BOOL=OFF `
         -D USE_SSE2_INSTRUCTIONS:BOOL=OFF `
         -D USE_SSE4_INSTRUCTIONS:BOOL=OFF `
         -D CMAKE_SYSTEM_VERSION=10.0 `
         -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
         -D _WINDLL=ON `
         -D _WIN32_UNIVERSAL_APP=ON `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_BLAS=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D DLIB_NO_GUI_SUPPORT=ON `
         ${CMakefileDir}" -ForegroundColor Yellow
         cmake -G "${vs}" -A $vsarch -T host=x64 `
               -D CMAKE_SYSTEM_NAME=WindowsStore `
               -D USE_AVX_INSTRUCTIONS:BOOL=OFF `
               -D USE_SSE2_INSTRUCTIONS:BOOL=OFF `
               -D USE_SSE4_INSTRUCTIONS:BOOL=OFF `
               -D CMAKE_SYSTEM_VERSION=10.0 `
               -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
               -D _WINDLL=ON `
               -D _WIN32_UNIVERSAL_APP=ON `
               -D DLIB_USE_CUDA=OFF `
               -D DLIB_USE_BLAS=OFF `
               -D DLIB_USE_LAPACK=OFF `
               -D DLIB_NO_GUI_SUPPORT=ON `
               ${CMakefileDir}
      }
      else
      {
         $USE_AVX_INSTRUCTIONS  = $Config.GetAVXINSTRUCTIONS()
         $USE_SSE4_INSTRUCTIONS = $Config.GetSSE4INSTRUCTIONS()
         $USE_SSE2_INSTRUCTIONS = $Config.GetSSE2INSTRUCTIONS()

         Write-Host "   cmake -G `"${vs}`" -A $vsarch -T host=x64 `
         -D CMAKE_SYSTEM_NAME=WindowsStore `
         -D CMAKE_SYSTEM_VERSION=10.0 `
         -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
         -D _WINDLL=ON `
         -D _WIN32_UNIVERSAL_APP=ON `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_BLAS=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D DLIB_NO_GUI_SUPPORT=ON `
         -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
         -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
         -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
         ${CMakefileDir}" -ForegroundColor Yellow
         cmake -G "${vs}" -A $vsarch -T host=x64 `
               -D CMAKE_SYSTEM_NAME=WindowsStore `
               -D CMAKE_SYSTEM_VERSION=10.0 `
               -D WINAPI_FAMILY=WINAPI_FAMILY_APP `
               -D _WINDLL=ON `
               -D _WIN32_UNIVERSAL_APP=ON `
               -D DLIB_USE_CUDA=OFF `
               -D DLIB_USE_BLAS=OFF `
               -D DLIB_USE_LAPACK=OFF `
               -D DLIB_NO_GUI_SUPPORT=ON `
               -D USE_AVX_INSTRUCTIONS=$USE_AVX_INSTRUCTIONS `
               -D USE_SSE4_INSTRUCTIONS=$USE_SSE4_INSTRUCTIONS `
               -D USE_SSE2_INSTRUCTIONS=$USE_SSE2_INSTRUCTIONS `
               ${CMakefileDir}
      }

   }
}

function ConfigANDROID([Config]$Config, [string]$CMakefileDir)
{
   if ($global:IsLinux)
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

      # https://github.com/Tencent/ncnn/wiki/FAQ-ncnn-throw-error#undefined-reference-to-__kmpc_xyz_xyz
      # $env:NDK_TOOLCHAIN_VERSION = 4.9
      $env:OpenCV_DIR = "${installOpenCVDir}/sdk/native/jni"
      $env:ncnn_DIR = "${installNcnnDir}/lib/cmake/ncnn"
         Write-Host "   cmake -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake `
      -D ANDROID_ABI=$abi `
      -D ANDROID_PLATFORM=android-$level `
      -D ANDROID_CPP_FEATURES:STRING=`"exceptions rtti`" `
      -D BUILD_SHARED_LIBS=ON `
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
      ${CMakefileDir}" -ForegroundColor Yellow
         cmake -D CMAKE_TOOLCHAIN_FILE=${env:ANDROID_NDK}/build/cmake/android.toolchain.cmake `
               -D ANDROID_ABI=$abi `
               -D ANDROID_PLATFORM=android-$level `
               -D ANDROID_CPP_FEATURES:STRING="exceptions rtti" `
               -D BUILD_SHARED_LIBS=ON `
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
               ${CMakefileDir}
   }
   else
   {
      Write-Host "Error: This platform can not build android binary" -ForegroundColor Red
      exit -1
   }
}

function ConfigIOS([Config]$Config, [string]$CMakefileDir)
{
   if ($global:IsMacOS)
   {
      # # Build NcnnDotNet.Native
      Write-Host "Start Build NcnnDotNet.Native" -ForegroundColor Green

      $developerDir = $Config.GetDeveloperDir()
      $osxArchitectures = $Config.GetOSXArchitectures()
      $toolchain = $Config.GetToolchainFile()

      $OSX_SYSROOT = $Config.GetIOSSDK($osxArchitectures, $developerDir)

      # use libc++ rather than libstdc++
      Write-Host "   cmake -D CMAKE_SYSTEM_NAME=iOS `
         -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
         -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
         -D CMAKE_TOOLCHAIN_FILE=`"${toolchain}`" `
         -D CMAKE_CXX_FLAGS=`"-std=c++11 -stdlib=libc++ -static`" `
         -D CMAKE_EXE_LINKER_FLAGS=`"-std=c++11 -stdlib=libc++ -static`" `
         -D BUILD_SHARED_LIBS=OFF `
         -D DLIB_USE_CUDA=OFF `
         -D DLIB_USE_BLAS=OFF `
         -D DLIB_USE_LAPACK=OFF `
         -D mkl_include_dir=`"`" `
         -D mkl_intel=`"`" `
         -D mkl_rt=`"`" `
         -D mkl_thread=`"`" `
         -D mkl_pthread=`"`" `
         -D LIBPNG_IS_GOOD=OFF `
         -D PNG_FOUND=OFF `
         -D PNG_LIBRARY_RELEASE=`"`" `
         -D PNG_LIBRARY_DEBUG=`"`" `
         -D PNG_PNG_INCLUDE_DIR=`"`" `
         -D DLIB_NO_GUI_SUPPORT=ON `
         ${CMakefileDir}" -ForegroundColor Yellow
      cmake -D CMAKE_SYSTEM_NAME=iOS `
            -D CMAKE_OSX_ARCHITECTURES=${osxArchitectures} `
            -D CMAKE_OSX_SYSROOT=${OSX_SYSROOT} `
            -D CMAKE_TOOLCHAIN_FILE="${toolchain}" `
            -D CMAKE_CXX_FLAGS="-std=c++11 -stdlib=libc++ -static" `
            -D CMAKE_EXE_LINKER_FLAGS="-std=c++11 -stdlib=libc++ -static" `
            -D BUILD_SHARED_LIBS=OFF `
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
            ${CMakefileDir}
   }
   else
   {
      Write-Host "Error: This platform can not build iOS binary" -ForegroundColor Red
      exit -1
   }
}

function Reset-Dlib-Modification([Config]$Config, [string]$currentDir)
{
   $dlibDir = $Config.GetDlibRootDir()
   Set-Location -Path $dlibDir
   Write-Host "Reset modification of ${dlibDir}" -ForegroundColor Yellow
   git checkout .
   Set-Location -Path $currentDir
}

function Build([Config]$Config)
{
   # current is each source directory
   $Current = Get-Location

   $CMakefile = Join-Path $Current "CMakeLists.txt"
   if (!(Test-Path(${CMakefile})))
   {
      Write-Host "CMakeLists.txt does not exist in ${Current}" -ForegroundColor Red
      exit -1
   }

   $Output = $Config.GetBuildDirectoryName("")
   if ((Test-Path $Output) -eq $False)
   {
      New-Item $Output -ItemType Directory
   }

   $BuildDirectory = $Config.GetStoreDriectory($Current)
   $BuildDirectory = Join-Path $BuildDirectory $Output
   if ((Test-Path $BuildDirectory) -eq $False)
   {
      New-Item $BuildDirectory -ItemType Directory
   }

   $Target = $Config.GetTarget()
   $Platform = $Config.GetPlatform()


   # revert dlib
   Reset-Dlib-Modification $Config (Join-Path $Current $Output)

   Set-Location -Path $BuildDirectory

   switch ($Platform)
   {
      "desktop"
      {
         switch ($Target)
         {
            "cpu"
            {
               ConfigCPU $Config $Current
            }
            "mkl"
            {
               ConfigMKL $Config $Current
            }
            "cuda"
            {
               ConfigCUDA $Config $Current
            }
            "arm"
            {
               ConfigARM $Config $Current
            }
         }
      }
      "android"
      {
         ConfigANDROID $Config $Current
      }
      "ios"
      {
         ConfigIOS $Config $Current
      }
      "uwp"
      {
         ConfigUWP $Config $Current
      }
   }

   $cofiguration = $Config.GetConfigurationName()
   Write-Host "   cmake --build . --config ${cofiguration}" -ForegroundColor Yellow
   cmake --build . --config ${cofiguration}

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

   $dstDir = Join-Path $dstDir runtimes | `
             Join-Path -ChildPath ${rid} | `
             Join-Path -ChildPath native

   $output = Join-Path $dstDir $libraryName

   if (!(Test-Path($binary)))
   {
      Write-Host "${binary} does not exist" -ForegroundColor Red
   }

   if (!(Test-Path($dstDir)))
   {
      Write-Host "${dstDir} does not exist" -ForegroundColor Red
   }

   Write-Host "Copy ${libraryName} to ${output}" -ForegroundColor Green
   Copy-Item ${binary} ${output}
}