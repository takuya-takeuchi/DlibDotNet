$mklDir = $env:MKL_WIN
$current = $PSScriptRoot

if (!(Test-Path "${mklDir}"))
{
    Write-Host "${mklDir} does not exist" -ForegroundColor Red
    exit -1
}

$libraries = @{
    "redist\ia32_win\compiler\libiomp5md.dll"="x86";
    "redist\ia32_win\mkl\mkl_avx2.dll"="x86";
    "redist\ia32_win\mkl\mkl_core.dll"="x86";
    "redist\ia32_win\mkl\mkl_intel_thread.dll"="x86";
    "redist\intel64_win\compiler\libiomp5md.dll"="x64";
    "redist\intel64_win\mkl\mkl_avx2.dll"="x64";
    "redist\intel64_win\mkl\mkl_core.dll"="x64";
    "redist\intel64_win\mkl\mkl_intel_thread.dll"="x64";
}

foreach($entry in $libraries.GetEnumerator())
{
    $key   = $entry.Key;
    $value = $entry.Value;
    $src = Join-Path $mklDir $key
    $dst = Join-Path $current $value

    if (!(Test-Path "${src}"))
    {
        Write-Host "${src} does not exist" -ForegroundColor Red
        exit -1
    }

    if (!(Test-Path "${dst}"))
    {
        Write-Host "${dst} does not exist" -ForegroundColor Red
        exit -1
    }

    Copy-Item ${src} ${dst}
}