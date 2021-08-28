$current = $PSScriptRoot
$src = Split-Path $current -Parent
$native = Join-Path $src DlibDotNet.Native | `
          Join-Path -ChildPath "dlib"
$nativeDnn = Join-Path $src DlibDotNet.Native.Dnn | `
             Join-Path -ChildPath "dlib"
$root = Join-Path $current dlib

# coomon
$sources = @(
    "shared.h",
    "template.h",
    "export.h",
    "vector_streambuf.cpp",
    "vector_streambuf.h"
)
foreach ($f in $sources)
{
    $s = Join-Path $native $f
    $d = Join-Path $root $f
    Copy-Item $s $d -Force
}

# matrix
$directory = "matrix"
$sources = @(
    "matrix.h",
    "matrix.cpp",
    "matrix_common.h",
    "matrix_common.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# cuda
$directory = "cuda"
$sources = @(
    "cuda.h",
    "cuda.cpp"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $native $directory | Join-Path -ChildPath $f
    $d = Join-Path $root   $directory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}

# dnn
$directory = "dnn"
$sources = @(
    "dnn"
)
$deletes = @(
    "input.cpp",
    "input.h",
    "tensor.cpp",
    "tensor.h",
    "validation.cpp",
    "validation.h",
    # "solvers/adam.cpp",
    # "solvers/adam.h",
    "solvers/sgd.cpp",
    "solvers/sgd.h",
    "loss/mmod",
    "loss/multiclass_log",
    "loss/multiclass_log_per_pixel"
)
New-Item (Join-Path $root $directory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $nativeDnn $directory | Join-Path -ChildPath "*"
    $d = Join-Path $root      $directory
    Copy-Item "${s}" "${d}" -Recurse -Force
}
foreach ($f in $deletes)
{
    $d = Join-Path $root $directory | Join-Path -ChildPath $f
    $item = Get-Item "${d}"
    if ($item -is [System.IO.DirectoryInfo])
    {
        Remove-Item "${d}" -Recurse
    }
    else
    {
        Remove-Item "${d}"
    }
}

# cuda
$directory = "cuda"
$newDirectory = "cuda_dnn"
$sources = @(
    "cuda.h",
    "cuda.cpp"
)
New-Item (Join-Path $root $newDirectory) -Type Directory -Force | Out-Null
foreach ($f in $sources)
{
    $s = Join-Path $nativeDnn $directory    | Join-Path -ChildPath $f
    $d = Join-Path $root      $newDirectory | Join-Path -ChildPath $f
    Copy-Item $s $d -Force
}