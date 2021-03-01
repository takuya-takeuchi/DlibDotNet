#***************************************
#Arguments
#%1: CUDA (92,100,101,102,110,111)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $CUDA
)

Set-StrictMode -Version Latest

# For windows

# For DlibDotNet.CUDA92
$tmp92 = New-Object 'System.Collections.Generic.List[string]'
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\cublas64_92.dll")
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\cudnn64_7.dll")
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\curand64_92.dll")
$tmp92.Add("$env:CUDA_PATH_V9_2\bin\cusolver64_92.dll")

# For DlibDotNet.CUDA100
$tmp100 = New-Object 'System.Collections.Generic.List[string]'
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\cublas64_100.dll")
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\cudnn64_7.dll")
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\curand64_100.dll")
$tmp100.Add("$env:CUDA_PATH_V10_0\bin\cusolver64_100.dll")

# For DlibDotNet.CUDA101
$tmp101 = New-Object 'System.Collections.Generic.List[string]'
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\cublas64_10.dll")
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\cudnn64_7.dll")
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\curand64_10.dll")
$tmp101.Add("$env:CUDA_PATH_V10_1\bin\cusolver64_10.dll")

# For DlibDotNet.CUDA102
$tmp102 = New-Object 'System.Collections.Generic.List[string]'
$tmp102.Add("$env:CUDA_PATH_V10_2\bin\cublas64_10.dll")
$tmp102.Add("$env:CUDA_PATH_V10_2\bin\cudnn64_7.dll")
$tmp102.Add("$env:CUDA_PATH_V10_2\bin\curand64_10.dll")
$tmp102.Add("$env:CUDA_PATH_V10_2\bin\cusolver64_10.dll")

# For DlibDotNet.CUDA110
$tmp110 = New-Object 'System.Collections.Generic.List[string]'
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cublas64_11.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cublasLt64_11.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn_adv_infer64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn_adv_train64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn_cnn_infer64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn_cnn_train64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn_ops_infer64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn_ops_train64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cudnn64_8.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\curand64_10.dll")
$tmp110.Add("$env:CUDA_PATH_V11_0\bin\cusolver64_10.dll")

# For DlibDotNet.CUDA111
$tmp111 = New-Object 'System.Collections.Generic.List[string]'
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cublas64_11.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cublasLt64_11.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn_adv_infer64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn_adv_train64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn_cnn_infer64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn_cnn_train64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn_ops_infer64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn_ops_train64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cudnn64_8.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\curand64_10.dll")
$tmp111.Add("$env:CUDA_PATH_V11_1\bin\cusolver64_11.dll")

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Package = "92";  Dependencies = $tmp92    }
$BuildTargets += New-Object PSObject -Property @{Package = "100"; Dependencies = $tmp100   }
$BuildTargets += New-Object PSObject -Property @{Package = "101"; Dependencies = $tmp101   }
$BuildTargets += New-Object PSObject -Property @{Package = "102"; Dependencies = $tmp102   }
$BuildTargets += New-Object PSObject -Property @{Package = "110"; Dependencies = $tmp110   }
$BuildTargets += New-Object PSObject -Property @{Package = "111"; Dependencies = $tmp111   }

# Store current directory
$DlibDotNetRoot = $PSScriptRoot

$Libraries = $Null
foreach ($Target in $BuildTargets)
{
    if ($Target.Package -eq $CUDA)
    {
        $Libraries = $Target.Dependencies
        break
    }
}

if ($Libraries -eq $Null)
{
    Write-Host "${CUDA} is invalid parameter" -ForegroundColor Red
    exit
}

$BuildConfiguration = "Release"

$BuildLibraryWindowsHash = 
@{
   "DlibDotNet.Native"     = (Join-Path $BuildConfiguration "DlibDotNetNative.dll");
   "DlibDotNet.Native.Dnn" = (Join-Path $BuildConfiguration "DlibDotNetNativeDnn.dll")
}

$SourceDir = Join-Path $DlibDotNetRoot src
$BuildDir = "build_win_desktop_cuda-${CUDA}_x64"

$Files = Get-ChildItem -Recurse -Name -include *.csproj

$re = New-Object regex("<TargetFramework>(?<Version>[^<]+)</TargetFramework>") 
foreach ($File in $Files)
{
    $ProjectRoot = Split-Path ${file} -Parent
    $ProjectRoot = Join-Path ${DlibDotNetRoot} ${ProjectRoot}

    # check framework version
    $ProjectFile = Get-Content ${file}
    $Match = $re.Matches(${ProjectFile})
    if ($Match -ne $Null)
    {
        $Version = $Match.Groups[1].Value
        
        $Configurations = @( "Release", "Debug" )
        foreach ($Configuration in $Configurations)
        {
            $TargetDir = Join-Path $ProjectRoot bin | `
                         Join-Path -ChildPath $Configuration | `
                         Join-Path -ChildPath $Version

            if (!(Test-Path $TargetDir))
            {
                New-Item $TargetDir -ItemType Directory | Out-Null
            }

            foreach ($Library in $Libraries)
            {
                $FileName = Split-Path $Library -Leaf
                $FilePath = Join-Path $TargetDir $FileName

                if ((Test-Path $FilePath))
                {
                    Remove-Item $FilePath
                }

                New-Item -Value "$Library" -Path "$TargetDir" -Name "$FileName" -ItemType SymbolicLink > $null
            }

            foreach ($key in $BuildLibraryWindowsHash.keys)
            {
                $SrcDir = Join-Path $SourceDir $key | `
                          Join-Path -ChildPath $BuildDir
                $Dll = $BuildLibraryWindowsHash[$key]
                $FileName = Split-Path $Dll -Leaf
                $FilePath = Join-Path $TargetDir $FileName
                $Library = Join-Path $SrcDir $Dll

                if ((Test-Path $FilePath))
                {
                    Remove-Item $FilePath
                }

                New-Item -Value "$Library" -Path "$TargetDir" -Name "$FileName" -ItemType SymbolicLink > $null
            }
        }
    }
}

# Move to Root directory
Set-Location -Path $DlibDotNetRoot