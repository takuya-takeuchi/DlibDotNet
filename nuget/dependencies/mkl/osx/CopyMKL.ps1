$mklDir = $env:MKL_WIN
$current = $PSScriptRoot

if (!(Test-Path "${mklDir}"))
{
    Write-Host "${mklDir} does not exist" -ForegroundColor Red
    exit -1
}

$libraries = @{
    "/opt/intel/compilers_and_libraries_2020.0.166/mac/mkl/lib/libmkl_rt.dylib"=""x64";
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