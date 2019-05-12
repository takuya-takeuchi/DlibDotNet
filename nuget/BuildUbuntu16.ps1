Param()

$Distribution="ubuntu-16"

# Store current directory
$Current = Get-Location
$DlibDotNetRoot = (Split-Path (Get-Location) -Parent)
$DlibDotNetSourceRoot = Join-Path $DlibDotNetRoot src
$DockerDir = Join-Path $Current docker

Set-Location -Path $DockerDir

$Configuration = "Release"
$TargetArray = @("cpu","cuda")
$CUDAVersionArray = @(92,100)
$ArchitectureArray = @(64)
$ArchitectureHash = @{32 = "x86"; 64 = "x64"}
$BuildSourceArray = @("DlibDotNet.Native", "DlibDotNet.Native.Dnn")
$BuildSourceHash = @{"DlibDotNet.Native" = "libDlibDotNetNative.so"; "DlibDotNet.Native.Dnn" = "libDlibDotNetNativeDnn.so"}

foreach($architecture in $ArchitectureArray)
{
  foreach($cuda in $CUDAVersionArray)
  {
    $cudaname = "cuda-" + $cuda
    $targetname = $Distribution + "_" + $cudaname
    $dockername = "dlibdotnet-" + $targetname

    if ((Test-Path Dockerfile.$targetname) -eq $False)
    {
      Write-Host "Dockerfile.$targetname is not found" -ForegroundColor Red
      continue
    }

    Write-Host "Build dockerfile [$targetname]" -ForegroundColor Green
    docker build -t $dockername -f Dockerfile.$targetname .

    # Only one time
    if ($cuda -eq 92)
    {
      Write-Host "Run $dockername [cpu]" -ForegroundColor Green
      docker run --name $dockername --rm `
                 -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
                 -t $dockername cpu $architecture
    }

    Write-Host "Run $dockername [cuda-$cuda]" -ForegroundColor Green
    docker run --name $dockername --rm `
               -v "$($DlibDotNetRoot):/opt/data/DlibDotNet" `
               -t $dockername cuda $architecture $cuda

    # Copy output binary

    foreach($source in $BuildSourceArray)
    {
      $dll = $BuildSourceHash[$source]
      $srcDir = Join-Path $DlibDotNetSourceRoot $source

      foreach($target in $TargetArray)
      {
        if ($target -eq "cuda")
        {
          $build = "build_linux_" + $target + "-" + $cuda + "_" + $ArchitectureHash[$architecture]
          $libraryDir = $target + "-" + $cuda
        }
        else
        {
          # Only one time
          if ($cuda -ne 92)
          {
            continue
          }

          $build = "build_linux_" + $target + "_" + $ArchitectureHash[$architecture]
          $libraryDir = $target
        }

        $binary = Join-Path $srcDir $build  | `
                  Join-Path -ChildPath $dll
        $output = Join-Path $Current $libraryDir  | `
                  Join-Path -ChildPath runtimes | `
                  Join-Path -ChildPath ("linux-" + $ArchitectureHash[$architecture]) | `
                  Join-Path -ChildPath native | `
                  Join-Path -ChildPath $dll

        Write-Host "`tCopy $dll to $output" -ForegroundColor Green
        Copy-Item $binary $output
      }
    }
  }
}

# Move to Root directory 
Set-Location -Path $Current